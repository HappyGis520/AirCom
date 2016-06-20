using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDSBulkFetcher.EDSDemoProxy;
using System.Xml.Serialization;
using System.Xml;
using System.Threading;

namespace EDSBulkFetcher
{
    /// <summary>
    /// Simple logging class, ideally should use MSEL Block instead. 
    /// </summary>
    public class Logger
    {
        internal void Error(string p, string LogSource)
        {
            Console.WriteLine("Error: {0}", p);
        }

        internal void Information(string p, string LogSource)
        {
            Console.WriteLine("Info: {0}", p);
        }

        internal void Exception(Exception _ex, string LogSource)
        {
            Console.WriteLine("Exception: {0}", _ex.ToString());
        }
    }

    public class EDSFetcherHelper
    {
        Logger Log = new Logger();
        readonly string LogSource = "Demo";        
        int m_edsRequestTimeout = 500;
        const int m_iBatchSize = 2;        

        public void HandleQuery(string sEDSQuery, QueryableTypes type, EDS client, int PageSize, Action<int, int, int> Progress)
        {                        
            try
            {                
                int recordsRemaining = 0;
                int offset = 0;
                bool bFirstTime = true;
                int iTotalRecords = 0;
                do
                {
                    QueryRequestType req = null;
                    if (bFirstTime)
                    {
                        req = new QueryRequestType()
                        {
                            QueryItems = new QueryItemType[] 
                            {
                                ConstructQueryItem(sEDSQuery, type, PageSize, offset)         
                            },
                            itemID = Guid.NewGuid(),
                            TimeoutOverride = m_edsRequestTimeout
                        };
                    }
                    else
                    {
                        List<QueryItemType> qItems = new List<QueryItemType>();
                        req = new QueryRequestType()
                        {
                            itemID = Guid.NewGuid(),
                            TimeoutOverride = m_edsRequestTimeout
                        };

                        for (int i = 0; i < m_iBatchSize; i++)
                        {
                            qItems.Add(ConstructQueryItem(sEDSQuery, type, PageSize, offset + (PageSize * i)));
                        }

                        req.QueryItems = qItems.ToArray();
                    }


                    DateTime start = DateTime.Now;
                    var EDSResponse = client.Query(req);
                    TimeSpan timespan = DateTime.Now.Subtract(start);
                    string sError = "";

                    bool bError = ValidateResponse(EDSResponse, sEDSQuery, type, req.QueryItems, out sError);

                    if (!bError && EDSResponse != null && EDSResponse.Data != null)
                    {
                        if (bFirstTime)
                        {
                            DataType d = EDSResponse.Data.First();
                            iTotalRecords = d.remaining + d.nextOffset;
                            bFirstTime = false;
                        }
                        
                        HandleData(EDSResponse);

                        var data = EDSResponse.Data.AsQueryable();
                        recordsRemaining = data.Min(d => d.remaining); 
                        offset = data.Max(d => d.nextOffset); 

                        if (Progress != null)
                        {
                            int iTotal = recordsRemaining + offset;

                            if (iTotal == 0)
                                Progress(100, iTotalRecords, iTotalRecords);
                            else
                            {
                                float percentage = ((float)offset / (float)iTotal) * 100;
                                Progress((int)percentage, offset, iTotal);
                            }
                        }
                    }
                    else
                    {
                        if (bError && Progress != null)
                        {
                            throw new Exception(sError);
                        }

                        break;
                    }

                } while (recordsRemaining > 0);

            }
            catch (Exception _ex)
            {
                Log.Exception(_ex, LogSource);
                throw;
            }            
        }        

        /// <summary>
        /// Dumps EDS response data to the console (example)
        /// </summary>
        /// <param name="data"></param>
        private void HandleData(QueryResponseType data)
        {
            // if outer result a success...
            if (data.Status.code == StatusTypeCode.OK)
            {
                // visit each individual response using the status ref as a lookup
                foreach (var s in data.Status.Status)
                {
                    if (s.code == StatusTypeCode.OK)
                    {
                        // use LINQ to match successful status result with matching dataset
                        DataType d = data.Data.Where(q => q.itemIDRef == s.@ref).FirstOrDefault();
                        if (d != null)
                        {
                            XmlSerializer sr = new XmlSerializer(typeof(DataType));
                            XmlWriterSettings settings = new XmlWriterSettings()
                            {
                                Indent = true,
                                NewLineHandling = NewLineHandling.Entitize
                            };

                            // Simply dump dataset to xml file to console...
                            using (XmlWriter wr = XmlWriter.Create(Console.Out, settings))
                            {
                                sr.Serialize(wr, d);
                            }
                        }
                    }
                    else
                    {
                        // Primitive error handling...
                        System.Diagnostics.Debug.WriteLine(string.Format("code: {0}, comment: {1}", s.code, s.comment));
                    }
                }
            }
            else
            {
                // Simply dump errors to diagnostic console. 
                foreach (var stat in data.Status.Status)
                    System.Diagnostics.Debug.WriteLine(string.Format("code: {0}, comment: {1}", stat.code, stat.comment));
            }
        }

        private bool ValidateResponse(QueryResponseType EDSResponse, string sEDSQuery, QueryableTypes type, QueryItemType[] queryItemType, out string sError)
        {
            if (EDSResponse == null || EDSResponse.Status == null || EDSResponse.Status.Status == null)
            {
                sError = "Either response, response.Status, or response.Status.Status was null";
                return true;
            }

            bool error = false;
            StringBuilder str = new StringBuilder();

            ValidateStatus(new StatusType[] { EDSResponse.Status }, str, ref error, queryItemType);

            if (error == true)
            {
                Log.Error(str.ToString(), LogSource);
            }

            sError = str.ToString();

            return error;

        }

        /// <summary>
        /// Validates the StatusType object.
        /// </summary>
        /// <param name="_status">The statuses to validate.</param>
        /// <param name="_str">Used to return detailed comments of failures.</param>
        /// <param name="_error">Indicates the succes of the validation. True is a failure and _str should be consulted for details.</param>
        /// <param name="_items">The data items that were used in the request. These are used for better error reporting.</param>
        /// <remarks>Anything other than StatusTypeCode.OK is appened to the _str parameter.</remarks>
        private void ValidateStatus(StatusType[] _status, StringBuilder _str, ref bool _error, DataItemBaseType[] _items)
        {
            if (_status != null)
            {
                foreach (StatusType status in _status)
                {
                    if (status.code != StatusTypeCode.OK)
                    {
                        _error = true;
                        DataItemBaseType item = _items.Where(it => it.itemID == status.@ref).FirstOrDefault();
                        string id = null;

                        if (item is CreateItemType)
                        {
                            CreateItemType cItem = item as CreateItemType;

                            if (cItem.NewData.Count() > 0)
                            {
                                id = String.Format(" [{0} : {1}]", cItem.NewData[0].GetType().Name, cItem.NewData[0].iid);
                            }
                        }
                        else if (item is ModifyItemType)
                        {
                            ModifyItemType mItem = item as ModifyItemType;

                            if (mItem.NewData.Count() > 0)
                            {
                                id = String.Format(" [{0} : {1}]", mItem.NewData[0].GetType().Name, mItem.NewData[0].iid);
                            }
                        }
                        else if (item is DeleteItemType)
                        {
                            DeleteItemType dItem = item as DeleteItemType;

                            if (dItem.Select != null && dItem.Select.Item is QrySimpleTextType)
                            {
                                id = String.Format(" [{0} : {1}]", dItem.objectType.ToString(), (dItem.Select.Item as QrySimpleTextType).Query);
                            }
                        }
                        else if (item is QueryItemType)
                        {
                            QueryItemType qItem = item as QueryItemType;

                            if (qItem.Select != null && qItem.Select.Item is QrySimpleTextType)
                            {
                                id = String.Format(" [{0} : {1}]", qItem.objectType.ToString(), (qItem.Select.Item as QrySimpleTextType).Query);
                            }
                        }

                        _str.Append(String.Format("## {0}{1} - {2}", status.code.ToString(), id == null ? "" : id, status.comment));
                        ValidateStatus(status.Status, _str, ref _error, _items);
                    }
                }
            }
        }


        /// <summary>
        /// Creates a fully formed QueryItemType wrapper
        /// </summary>
        /// <param name="sEDSQuery">EDS query string</param>
        /// <param name="type">EDS object type</param>
        /// <param name="PageSize">Number of records EDS needs to fetch</param>
        /// <param name="offset">Starting row within dataset</param>
        /// <returns></returns>
        private static QueryItemType ConstructQueryItem(string sEDSQuery, QueryableTypes type, int PageSize, int offset)
        {
            return new QueryItemType()
            {
                itemID = Guid.NewGuid(),
                objectType = type,
                Select = new QrySelectType()
                {
                    Item = new QrySimpleTextType()
                    {
                        // Place our query expression here...
                        Query = string.Format(sEDSQuery)
                    }
                },
                count = PageSize,
                offset = offset
            };
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {            
            EDSDemoProxy.EDSClient client = new EDSBulkFetcher.EDSDemoProxy.EDSClient("EDSWCFNetTCPEndPoint");
            client.Open();

            try
            {
                EDSFetcherHelper fetcher = new EDSFetcherHelper();
                fetcher.HandleQuery(string.Format("iid st \"{0}\" and bvid = \"{1}\"", "SF03", "West"), QueryableTypes.UMTSCellType, client, 100, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception: {0}", e.ToString());
            }
            client.Close();
            
        }
                   
    }
}
