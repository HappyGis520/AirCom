using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENStoEDSDemo.EDSServiceProxy;
using System.Xml.Serialization;
using System.Xml;

namespace ENStoEDSDemo
{
    internal class EDSMessageDispatcher
    {
        EDSServiceProxy.EDSClient _client;
        public EDSMessageDispatcher()
        {
            _client = new ENStoEDSDemo.EDSServiceProxy.EDSClient("EDSWCFNetTCPEndPoint");
            _client.Open();
        }


        /// <summary>
        /// Builds an EDS query and dumps the result to disk if successful...
        /// </summary>
        /// <param name="notificationData"></param>
        internal void RequestData(NotificationData notificationData)
        {

            // EDS Query's require an outer request structure with a unique (guid) id and one request item per task
            QueryRequestType qry = new QueryRequestType()
            {
                itemID = Guid.NewGuid(),    // Outer request id
                QueryItems = new QueryItemType[]
                {
                    new QueryItemType()
                    {
                        itemID = Guid.NewGuid(),    // Inner request id for an item
                        // data type to query against
                        objectType = (QueryableTypes)Enum.Parse(typeof(QueryableTypes), notificationData.ObjectTypeString), 
                        // build a freeform text query
                        Select = new QrySelectType()
                        {
                            Item = new QrySimpleTextType()
                            {
                                // Place our query expression here...
                                Query = string.Format("iid = \"{0}\" and bvid = \"{1}\"", notificationData.ObjectID, notificationData.Project)
                            }
                        }
                    }
                }
            };
            
            // sends query
            QueryResponseType data = _client.Query(qry);

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

                            // Simply dump dataset to xml file on disk...
                            using (XmlWriter wr = XmlWriter.Create(notificationData.ObjectTypeString + "-" + d.itemIDRef.ToString() + ".xml", settings))
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
    }
}
