using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using JLIB.Utility;
using SimpleEDSQuery.EDSDemoProxy;

namespace SimpleEDSQuery
{
    internal  class BLLLTE
    {
        public BLLLTE()
        {
            LTENodeType node = new LTENodeType();
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Nodes"></param>
        /// <param name="xmlFileName"></param>
        public void BuildDeleteLocationXml(List<RootEntityType> Nodes,string xmlFileName )
        {
                var nsMgr = GetXmlSerializerNamespaces();

                 XmlSerializer sr = new XmlSerializer(typeof(List<RootEntityType>));
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = ("\t\t"),
                    NamespaceHandling = NamespaceHandling.OmitDuplicates,
                    NewLineHandling = NewLineHandling.Entitize
                };

                using (XmlWriter wr = XmlWriter.Create(xmlFileName, settings))
                {
                    sr.Serialize(wr,Nodes,nsMgr );
                } 
             
        }

        private static XmlSerializerNamespaces GetXmlSerializerNamespaces()
        {
            XmlSerializerNamespaces nsMgr = new XmlSerializerNamespaces();
            nsMgr.Add("umts70", "http://www.aircominternational.com/Schemas/UMTS/2010/08");
            nsMgr.Add("gsm", "http://www.aircominternational.com/Schemas/GSM/2009/09");
            nsMgr.Add("eqp", "http://www.aircominternational.com/Schemas/Equipment/2009/09");
            nsMgr.Add("umts", "http://www.aircominternational.com/Schemas/UMTS/2010/07");
            nsMgr.Add("tra70", "http://www.aircominternational.com/Schemas/Connect/2010/08");
            nsMgr.Add("co", "http://www.aircominternational.com/Schemas/Common/2009/07");
            nsMgr.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nsMgr.Add("co70", "http://www.aircominternational.com/Schemas/Common/2010/08");
            nsMgr.Add("gsm70", "http://www.aircominternational.com/Schemas/GSM/2010/08");
            nsMgr.Add("util", "http://www.aircominternational.com/contract/Util/2009/10");
            nsMgr.Add("config", "http://www.aircominternational.com/Schemas/Configuration/2010/08");
            nsMgr.Add("tra", "http://www.aircominternational.com/Schemas/Connect/2009/09");
            nsMgr.Add("ct", "http://www.aircominternational.com/Schemas/CommonTypes/2009/05");
            nsMgr.Add("lte", "http://www.aircominternational.com/Schemas/LTE/2010/08");
            nsMgr.Add("eqp70", "http://www.aircominternational.com/Schemas/Equipment/2010/08");
            nsMgr.Add("umts2", "http://www.aircominternational.com/Schemas/UMTS/2009/05");
            return nsMgr;
        }

        public void BuildCrateLocationXml(List<RootEntityType> Nodes, string xmlFileName)
        {
            var nsMgr = GetXmlSerializerNamespaces();
            RootEntityType aa = new RootEntityType();
           

            XmlSerializer sr = new XmlSerializer(typeof(List<RootEntityType>));
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = ("\t\t"),
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
                NewLineHandling = NewLineHandling.Entitize
            };

            using (XmlWriter wr = XmlWriter.Create(xmlFileName, settings))
            {
                sr.Serialize(wr, Nodes, nsMgr);
            }
        }


        public void GetAllLTENodeREQ()
        {
            QueryItemType Param_Item = new QueryItemType();
           Param_Item.itemID = new Guid();
            Param_Item.objectType = QueryableTypes.LTENodeType;

            QueryRequestType Params = new QueryRequestType();
            Params.QueryItems = new QueryItemType[] { Param_Item};
            Params.itemID = new Guid();

            EDSClient client = new EDSClient("EDSWCFNetTCPEndPoint");
            client.Open();
            var Respone =   client.Query(Params);
            if (Respone.Status.code == StatusTypeCode.OK)
            {
                JLog.Instance.AppDebug("查询成功");
                #region 输出成功

                foreach (var s in Respone.Status.Status)
                {
                    #region 访问所有的状态
                    if (s.code == StatusTypeCode.OK)
                    {

                        
                        // use LINQ to match successful status result with matching dataset
                        DataType d = Respone.Data.Where(q => q.itemIDRef == s.@ref).FirstOrDefault();//获取编号相同
                        
                        if (d != null)
                        {
                            JLog.Instance.AppDebug(string.Format("共获取到{0}个LTENODE", d.AppData.Count()));
                            //XmlSerializer sr = new XmlSerializer(typeof(DataType));
                            //XmlWriterSettings settings = new XmlWriterSettings()
                            //{
                            //    Indent = true,
                            //    NewLineHandling = NewLineHandling.Entitize
                            //};

                            //// Simply dump dataset to xml file on disk...
                            //using (XmlWriter wr = XmlWriter.Create("wjj" + "-" + d.itemIDRef.ToString() + ".xml", settings))
                            //{
                            //    sr.Serialize(wr, d);
                            //}
                        }
                    }
                    else
                    {
                        JLog.Instance.AppInfo(string.Format("code: {0}, comment: {1}", s.code, s.comment));
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                JLog.Instance.AppDebug("查询失败");
            }



        }

        
    }
}
