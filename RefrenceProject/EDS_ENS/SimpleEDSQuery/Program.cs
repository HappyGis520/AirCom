using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using SimpleEDSQuery.EDSDemoProxy;
using System.Xml.Serialization;
using System.Xml;
using JLIB.Utility;

namespace SimpleEDSQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LTECellType _lte = new LTECellType();
                LTENodeType _lteNode = new LTENodeType();
                

                LTEAntennaType _nType = new LTEAntennaType();
               

                LTECellFeederType feeder = new LTECellFeederType();
                

                //_nType.Feeders
                
                #region 序列化示例　
                
                RootEntityType Root1 = new RootEntityType();

        

                Root1.iid = "HL - D3900256154PT";
                Root1.bvid = "LTE_HUNAN";
                Root1.Security = null;


                RootEntityType Root2 = new RootEntityType();

                Root2.iid = "HL-D3900256155PT";
                Root2.bvid = "LTE_HUNAN";
                


                XmlSerializerNamespaces nsMgr = new XmlSerializerNamespaces();
                //XmlNamespaceManager nsMgr = new XmlNamespaceManager();//这一步实例化一个xml命名空间管理器

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

                //LTECellType Root1 = new LTECellType();
                //Root1.iid = "HL - D3900256154PT";
                //Root1.bvid = "LTE_HUNAN";
                //LTECellType Root2 = new LTECellType();
                //Root2.iid = "HL-D3900256155PT";
                //Root2.bvid = "LTE_HUNAN";
                List<RootEntityType> tt = new List<RootEntityType>();
                tt.Add(Root1);
                tt.Add(Root2);

                //JLIB.CSharp.JFileExten.ToXML((RootEntityType)Lte, "d:\\TestLte.xml");
                //XmlRootAttribute rootAtrAttribute = new XmlRootAttribute("ArrayOfRootEntityType");

                XmlAttributeAttribute aa = new XmlAttributeAttribute("type",typeof(string));
                aa.Namespace = "http://www.w3.org/2001/XMLSchema-instance";
                aa.AttributeName = typeof (LocationObjectv70Type).ToString();

              


                XmlSerializer sr = new XmlSerializer(typeof(List<RootEntityType>));
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = ("\t\t"),
                    NamespaceHandling = NamespaceHandling.OmitDuplicates,
                    NewLineHandling = NewLineHandling.Entitize
                    //NewLineOnAttributes = true

                };
                

                // Simply dump dataset to xml file on disk...
                using (XmlWriter wr = XmlWriter.Create("d:\\TestLte.xml", settings))
                {
                    sr.Serialize(wr,tt,nsMgr );
                } 
                #endregion

                #region 创建示例
                //EDSDemoProxy.EDSClient client = new SimpleEDSQuery.EDSDemoProxy.EDSClient("EDSWCFNetTCPEndPoint");
                //client.Open();
                 
                //List<RootEntityType> datas = new List<RootEntityType>();

                //var Entity = new LTENodeType();
                //Entity.iid = "";
                //Entity.bvid = "";
                //Entity.PLMN = new IDType()
                //{
                //    iid = "LTE_HUNAN"
                //};
                ////Entity.Cells;
                ////Entity.Antennas
                ////Entity.Carriers;
                //datas.Add(Entity);


                //#region LTE CELL

                //LTECellType Cell = new LTECellType();
             
                 
                //#endregion

                //CreateRequestType CreateParams = new CreateRequestType();
                //var CreateItems = new CreateItemType[]
                //{
                //    new CreateItemType()
                //    {
                //        itemID = new Guid(),
                //        objectType = QueryableTypes.LTENodeType,
                //        NewData = datas.ToArray()

                //    }
                //};
                //CreateParams.CreateItems = CreateItems;


                //CreateParams.CreateItems = CreateItems;


                //var Result = client.Create(CreateParams);


                #endregion

                #region wjj删除示例

                // var _DelItems = new List<DeleteItemType>();

                // #region 添加删除项
                // var _DelItem = new DeleteItemType();
                // _DelItem.objectType = QueryableTypes.LocationObjectv70Type;


                // #region 删除条件
                // var selType = new QrySelectType();
                // var objs  = new QrySimpleListType();
                // selType.Item = objs;
                // #region 删除对像集合
                // List<ListItemType> Itemss = new List<ListItemType>();

                //  var obj = new ListItemType()
                //  {
                //      bvid = "LTE_HUNAN",
                //      iid = "衡阳常宁市六联HL-D3900256154PT"
                //  };
                //  Itemss.Add(obj);
                //  var obj2 = new ListItemType()
                //  {
                //      bvid = "LTE_HUNAN",
                //      iid = "衡阳常宁市双湖HL-D3900256155PT"
                //  };
                //  Itemss.Add(obj2);


                // #endregion
                //objs.Items = Itemss.ToArray();


                // #endregion



                // _DelItem.Select = selType;


                // _DelItems.Add(_DelItem); 
                // #endregion

                //  var _QrySel = new QrySelectType();
                //  DeleteRequestType DelParams = new DeleteRequestType();
                //  DelParams.DeleteItems = _DelItems.ToArray();
                //  EDSDemoProxy.EDSClient client = new SimpleEDSQuery.EDSDemoProxy.EDSClient("EDSWCFNetTCPEndPoint");
                //  client.Open();
                //  client.Delete(DelParams);

                #endregion

                #region 自带查询示例

                //EDSDemoProxy.EDSClient client = new SimpleEDSQuery.EDSDemoProxy.EDSClient("EDSWCFNetTCPEndPoint");
                //client.Open();




                //var QueryComm = new QrySelectType()
                //{
                //    #region QrySelectType
                //    Item = new QrySimpleTextType()
                //    {
                //        // 查询表达式 --生成任意多边形指令
                //        Query = string.Format("iid = \"{0}\" and bvid = \"{1}\"", "SF03003A_11", "West")
                //    }
                //    #endregion
                //};

                //var QueryItemsInfo = new QueryItemType[]
                //{
                //    new QueryItemType()
                //    {
                //        itemID = Guid.NewGuid(), // Inner request id for an item
                //        // data type to query against
                //        objectType = ty,
                //        Select = QueryComm
                //    }
                //};


                //QueryRequestType qry = new QueryRequestType()

                //{
                //    itemID = Guid.NewGuid(),    // Outer request id
                //    QueryItems = QueryItemsInfo
                //};

                //// sends query
                //QueryResponseType data = client.Query(qry);




                //SerializeResultToDisk(data, ty); 

                #endregion

            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }




        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ty"></param>
        static void SerializeResultToDisk(QueryResponseType data, QueryableTypes ty)
        {
            if (data.Status.code == StatusTypeCode.OK)
            {
                #region 输出成功
                
                foreach (var s in data.Status.Status)
                {
                    #region 访问所有的状态
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
                            using (XmlWriter wr = XmlWriter.Create(ty + "-" + d.itemIDRef.ToString() + ".xml", settings))
                            {
                                sr.Serialize(wr, d);
                            }
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
                foreach (var stat in data.Status.Status)
                    System.Diagnostics.Debug.WriteLine(string.Format("code: {0}, comment: {1}", stat.code, stat.comment));
            }
        }
    }
}
