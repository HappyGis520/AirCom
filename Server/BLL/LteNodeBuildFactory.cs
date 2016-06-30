using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using EDSProxy.EDSClient;
using NetPlan.Model;
using JLIB.Utility;

namespace NetPlan.BLL
{
    public class LteNodeBuildFactory
    {
        private static System.Diagnostics.Process _EDSEcriptorProcess = null;

        #region XML文件生成
        /// <summary>
        /// 生成基站xml数据
        /// </summary>
        /// <param name="LTEStations"></param>
        public static bool  BuildLteNodeXML(List<RootEntityType> LTEStations, string SaveDir,out string XmlFileFullName)
        {

            try
            {
                #region 序列化示例
                XmlSerializerNamespaces nsMgr = new XmlSerializerNamespaces();

                #region 指定命名空间

                nsMgr.Add("umts", "http://www.aircominternational.com/Schemas/UMTS/2010/07");
                nsMgr.Add("tra70", "http://www.aircominternational.com/Schemas/Connect/2010/08");
                nsMgr.Add("ct", "http://www.aircominternational.com/Schemas/CommonTypes/2009/05");
                nsMgr.Add("co", "http://www.aircominternational.com/Schemas/Common/2009/07");
                nsMgr.Add("gsm80", "http://www.aircominternational.com/Schemas/GSM/2011/04");
                nsMgr.Add("util", "http://www.aircominternational.com/contract/Util/2009/10");
                nsMgr.Add("gsm70", "http://www.aircominternational.com/Schemas/GSM/2010/08");
                nsMgr.Add("config", "http://www.aircominternational.com/Schemas/Configuration/2010/08");
                nsMgr.Add("cdma", "http://www.aircominternational.com/Schemas/CDMA/2010/12");
                nsMgr.Add("co70", "http://www.aircominternational.com/Schemas/Common/2010/08");
                nsMgr.Add("ecs", "http://www.aircominternational.com/Schemas/EWS/ECSCoverageTypes/2011/08");
                nsMgr.Add("umts80", "http://www.aircominternational.com/Schemas/UMTS/2011/04");
                nsMgr.Add("eqp70", "http://www.aircominternational.com/Schemas/Equipment/2010/08");
                nsMgr.Add("eds", "http://www.aircominternational.com/contract/EDS/2009/05");
                nsMgr.Add("co80", "http://www.aircominternational.com/Schemas/Common/2011/04");
                nsMgr.Add("eqp80", "http://www.aircominternational.com/Schemas/Equipment/2011/04");
                nsMgr.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                nsMgr.Add("umts70", "http://www.aircominternational.com/Schemas/UMTS/2010/08");
                nsMgr.Add("tra", "http://www.aircominternational.com/Schemas/Connect/2009/09");
                nsMgr.Add("lte", "http://www.aircominternational.com/Schemas/LTE/2010/08");
                nsMgr.Add("umts2", "http://www.aircominternational.com/Schemas/UMTS/2009/05");
                nsMgr.Add("tra80", "http://www.aircominternational.com/Schemas/Connect/2011/04");
                nsMgr.Add("eqp", "http://www.aircominternational.com/Schemas/Equipment/2009/09");
                nsMgr.Add("gsm", "http://www.aircominternational.com/Schemas/GSM/2009/09");
                nsMgr.Add("lte80", "http://www.aircominternational.com/Schemas/LTE/2011/04");
                #endregion

                Type[] knowTypes = new[]
                {
                typeof (LTENodeType)
                //typeof(LTECellType),
                //typeof(LTENodeCarrierType),
                //typeof(LTEAntennaType),
                //typeof(LocationTypeType),
                //typeof(PredictionModelReferenceType)
            };

                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = ("\t\t"),
                    NamespaceHandling = NamespaceHandling.OmitDuplicates,
                    NewLineHandling = NewLineHandling.Entitize
                    //NewLineOnAttributes = true
                };


                XmlSerializer sr = new XmlSerializer(typeof(List<RootEntityType>), knowTypes);

                XmlFileFullName = string.Format("{1}\\LTENodeTypes_tpl_{0}.xml",
                   DateTime.Now.ToString("yyyymmddHHMMss"), SaveDir);
                using (XmlWriter wr = XmlWriter.Create(XmlFileFullName, settings))
                {
                    sr.Serialize(wr, LTEStations, nsMgr);
                }
                return true;

                #endregion
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                XmlFileFullName = string.Empty;
                return false;
            }


        }
        /// <summary>
        /// 生成删除基站xml数据
        /// </summary>
        /// <param name="LTEStations"></param>
        public static bool  BuilLteNodeDeleteXML(List<LTENodeType> LTEStations, string SaveDir ,out string XmlFileFullName)
        {
            try
            {
                #region 序列化示例
                XmlSerializerNamespaces nsMgr = new XmlSerializerNamespaces();

                #region 指定命名空间
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

                #endregion

                Type[] knowTypes = new[]
                {
                typeof(LTECellType)
            };
                List<RootEntityType> Cells = new List<RootEntityType>();
                foreach (var obj in LTEStations)
                {
                    foreach (var cell in obj.Cells)
                    {
                        LTECellType Newtype = new LTECellType()
                        {
                            bvid = obj.iid,
                            iid = cell.iid,
                        };
                        Cells.Add(Newtype);
                    }
                }

                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = ("\t\t"),
                    NamespaceHandling = NamespaceHandling.OmitDuplicates,
                    NewLineHandling = NewLineHandling.Entitize
                };


                XmlSerializer sr = new XmlSerializer(typeof(List<RootEntityType>), knowTypes);
                XmlFileFullName = string.Format("{1}\\LTENodeTypes_deleted_tpl_{0}.xml",
                    DateTime.Now.ToString("yyyyMMddHHmmss"), SaveDir);
                using (XmlWriter wr = XmlWriter.Create(XmlFileFullName, settings))
                {
                    sr.Serialize(wr, Cells, nsMgr);
                }

                return true;

                #endregion
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                XmlFileFullName = string.Empty;
                return false;
            }
        }
        /// <summary>
        /// 生成位置信息XML数据
        /// </summary>
        /// <param name="BaseInfo"></param>
        public static bool BuildLocationXML(AirComLTENodeBaseInfo BaseInfo, string SaveDir, out string XmlFileFullName)
        {
            try
            {
                LocationObjectv70Type location = new LocationObjectv70Type();
                location.Latitude = BaseInfo.Lat;
                location.LatitudeSpecified = true;
                location.Longitude = BaseInfo.Lng;
                location.LongitudeSpecified = true;
                location.iid = BaseInfo.StationId;

                location.AllowedOperations = ReadOnlyType.ReadWrite;

                List<RootEntityType> Locations = new List<RootEntityType>();
                Locations.Add(location);
                #region 序列化示例
                XmlSerializerNamespaces nsMgr = new XmlSerializerNamespaces();

                #region 指定命名空间
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
                #endregion

                Type[] knowTypes = new[]
                {
                typeof (LocationObjectv70Type),
                //typeof(LTENodeCarrierType),
                //typeof(LTEAntennaType),
                typeof(LocationTypeType)
                //typeof(PredictionModelReferenceType)
            };

                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = ("\t\t"),
                    NamespaceHandling = NamespaceHandling.OmitDuplicates,
                    NewLineHandling = NewLineHandling.Entitize
                    //NewLineOnAttributes = true
                };


                XmlSerializer sr = new XmlSerializer(typeof(List<RootEntityType>), knowTypes);

                XmlFileFullName = string.Format("{1}\\LTELocationTypes_tpl_{0}.xml", DateTime.Now.ToString("yyyymmddHHMMss"),
                    SaveDir);
                using (XmlWriter wr = XmlWriter.Create(XmlFileFullName, settings))
                {
                    sr.Serialize(wr, Locations, nsMgr);
                }

                return true;

                #endregion
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                XmlFileFullName = string.Empty;
                return false;

            }

        }
        /// <summary>
        /// 生成位置信息删除XML数据
        /// </summary>
        /// <param name="BaseInfo"></param>
        public static bool  BuildLocationDeleteXML(AirComLTENodeBaseInfo BaseInfo, string SaveDir , out string XmlFileFullName)
        {

            try
            {
                LocationObjectv70Type location = new LocationObjectv70Type();
                location.bvid = GlobalInfo.Instance.ConfigParam.ProjectNames[0].ProjectName;

                location.iid = BaseInfo.StationId;
                List<RootEntityType> Locations = new List<RootEntityType>();
                Locations.Add(location);
                #region 序列化示例
                XmlSerializerNamespaces nsMgr = new XmlSerializerNamespaces();

                #region 指定命名空间
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
                #endregion

                Type[] knowTypes = new[]
                {
                typeof (LocationObjectv70Type),
                //typeof(LTENodeCarrierType),
                //typeof(LTEAntennaType),
                typeof(LocationTypeType)
                //typeof(PredictionModelReferenceType)
            };

                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = ("\t\t"),
                    NamespaceHandling = NamespaceHandling.OmitDuplicates,
                    NewLineHandling = NewLineHandling.Entitize
                    //NewLineOnAttributes = true
                };


                XmlSerializer sr = new XmlSerializer(typeof(List<RootEntityType>), knowTypes);

                XmlFileFullName = string.Format("{1}\\LTELocationTypes_deleted_tpl_{0}.xml", DateTime.Now.ToString("yyyymmddHHMMss"),
                        SaveDir);
                using (XmlWriter wr = XmlWriter.Create(XmlFileFullName, settings))
                {
                    sr.Serialize(wr, Locations, nsMgr);
                }

                return true;

                #endregion
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                XmlFileFullName = string.Empty;
                return false;


            }


        } 
        #endregion

        /// <summary>
        /// 初始化执行EDS脚本参数
        /// </summary>
        /// <param name="LoadFilePath"></param>
        /// <param name="CommonType"></param>
        /// <param name="XmlFilePath"></param>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public static bool InitionRunAutoEDSScript(string LoadFilePath,EnumExcutEDSType CommonType, string XmlFilePath,string ProjectName)
        {
                string AutoEDSScrip = "AUTOEDSEXE";
                string appFileFullName = GlobalInfo.Instance.ConfigParam.EDSLoadAppFile; ;

                try
                {
                    //检测程序是否已启动，如果已启动，则退出后再启动
                    System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcessesByName(AutoEDSScrip);

                    if (processList != null && processList.Length > 0)
                    {
                        processList[0].Kill();
                        processList[0].WaitForExit(1000);
                    }

                    _EDSEcriptorProcess = null;
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(appFileFullName);
                    #if RELEASE
                    psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    #else
                    psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                    #endif
                    _EDSEcriptorProcess = System.Diagnostics.Process.Start(psi);
                    if (CommonType != EnumExcutEDSType.read)
                    {
                        _EDSEcriptorProcess.StartInfo.Arguments = string.Format(" -{0} -input=\"{1}\" -bvid=\"{2}\"",
                            CommonType.ToString(), XmlFilePath, ProjectName);
                    }
                    else
                    {
                        _EDSEcriptorProcess.StartInfo.Arguments = string.Format(" -{0} -type=\"LocationObjectType\" -output=\"{1}\" -bvid=\"{2}\"",
                                                CommonType.ToString(), XmlFilePath, ProjectName);
                    }

                    if (_EDSEcriptorProcess != null)
                    {
                        return true;
                    }
                    return false;
                }
                catch (System.Exception ex)
                {
                    JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                        MethodBase.GetCurrentMethod().Module.Name);
                    return false;
                }
            
        }



        /// <summary>
        /// 生成XML文件 
        /// </summary>
        /// <param name="BaseInfo">站点基本信息</param>
        /// <param name="Sectors">扇区信息，每个扇区的天线信息</param>
        /// <param name="AllAntennas">所有天线的信息</param>
        /// <param name="Savedir">保存目录</param>
        /// <returns></returns>
        public static bool BuilLTEXMLFilesInterface(AirComLTENodeBaseInfo BaseInfo, List<CellSector> Sectors,  string Savedir,out XmlFilePackageInfo XmlFiles)
        {
            try
            {
                LTENodeType _LteNode = new LTENodeType();

                //var keys = Sectors.Keys.ToList();

                List<AirComAntennaType> AllAntennas = new List<AirComAntennaType>();
                List<AirComAntennaType> SubAntennas = new List<AirComAntennaType>();
                foreach (var Sector in Sectors)
                {
                    //Sectors.TryGetValue(key, out SubAntennas);
                    AllAntennas.AddRange(Sector.Antenners as List<AirComAntennaType>);

                }
                BuildStationInfo(BaseInfo, ref _LteNode);

                BuildCarrierInfo(Sectors, ref _LteNode);

                BuildAntennaInfo(AllAntennas, ref _LteNode);

                BuildCellInfo(Sectors, ref _LteNode);

                #region 生成XML
                //XmlFiles = new XmlFilePackageInfo();
                return CreateXMLFiles(BaseInfo, _LteNode, Savedir, out XmlFiles);

                #endregion


            }

            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                XmlFiles = null;
                return false;
            }

        }


        #region XML文件生成
        /// <summary>
        /// 生成XML文件 
        /// </summary>
        /// <param name="BaseInfo"></param>
        /// <param name="_LteNode"></param>
        private static bool CreateXMLFiles(AirComLTENodeBaseInfo BaseInfo, LTENodeType _LteNode,string Savedir,out XmlFilePackageInfo XmlFiles)
        {
            //try
            //{
                List<RootEntityType> Nodes = new List<RootEntityType>();
                List<LTENodeType> nodesList = new List<LTENodeType>();
                Nodes.Add(_LteNode);
                nodesList.Add(_LteNode);
                Savedir = GlobalInfo.Instance.ConfigParam.XmlFileSaveDir.Trim();
                if (!Directory.Exists(Savedir))
                {
                    DirectoryInfo dirInfo = Directory.CreateDirectory(Savedir);
                }
                XmlFiles = new XmlFilePackageInfo();
                JLog.Instance.AppInfo("开始生成导入基站XML文件");
                BuildLteNodeXML(Nodes, Savedir, out XmlFiles.InputLTENodeFileFullName);
            　　JLog.Instance.AppInfo("开始生成删除基站XML文件");
            　　BuilLteNodeDeleteXML(nodesList, Savedir, out XmlFiles.DeleteLTENodeFileFullName);
                JLog.Instance.AppInfo("开始生成导入位置信息XML文件");
                BuildLocationXML(BaseInfo, Savedir, out XmlFiles.InputLocationFileFullName);
                JLog.Instance.AppInfo("开始生成删除位置信息XML文件");
                BuildLocationDeleteXML(BaseInfo, Savedir, out XmlFiles.DeleteLocationFileFullName);
                return true;
            //}
            //catch (Exception ex)
            //{
            //    JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
            //        MethodBase.GetCurrentMethod().Module.Name);
            //    XmlFiles = new XmlFilePackageInfo();
            //    return false;
            //}

        } 
        #endregion

        #region 数据注入方法

        /// <summary>
        /// 注入基础信息至RefLteNode
        /// </summary>
        /// <param name="BaseInfo">AirComLTENodeBaseInfo类型的用户输入的基站信息</param>
        /// <param name="RefLteNode">新生成的，EDS中的基站信息--LTENodeType类型</param>
        private static void BuildStationInfo(AirComLTENodeBaseInfo BaseInfo, ref LTENodeType RefLteNode)
        {
            #region 基本信息

            RefLteNode.iid = BaseInfo.StationId;
            RefLteNode.Location = new IDType()
            {
                iid = BaseInfo.StationId//唯一识别，同_LteNode.iid一致  //基站ID
            };
            //RefLteNode.Comments = GlobalInfo.Instance.ConfigParam.ProjectNames[0].CityName; //地市--可有可无---毕工确认  @20150808  ---（wjj:根据登录用户获取地市及工程名称）
            RefLteNode.Comments = BaseInfo.CityName; //地市--可有可无---毕工确认  @20150808  ---（wjj:根据登录用户获取地市及工程名称）--由浪潮提供@2016-04-03。
            RefLteNode.PLMN = new IDType()
            {
                //iid = GlobalInfo.Instance.ConfigParam.ProjectNames[0].ProjectName//工程同名---这个是必须的同---毕工确认  @20150808//
                //iid = "plan"//固定值plan---郭琴确认  @2015１１２７//
                iid = "PLMN0"//固定值plan---郭琴确认  @201606２2//
                //工程名是定死的，用户可选择地市信息，系统根据地市信息获取工程名（可配置）
            };
            RefLteNode.Name2 = BaseInfo.PlanPrjID.ToString();//填基站的规划期数；规划期数由用户在界面上输入。
            //RefLteNode.NodeID = 0;//毕工确认  @20150808 //wjj20150930 可忽略的值不用输入，序列化时会忽略
            
            #endregion
        }
        private static void BuildStationInfo(EtBaseStation BaseInfo, ref LTENodeType RefLteNode)
        {
            #region 基本信息

            RefLteNode.iid = BaseInfo.StationGUID;
            RefLteNode.Location = new IDType()
            {
                iid = BaseInfo.StationGUID//唯一识别，同_LteNode.iid一致  //基站ID
            };
            RefLteNode.Comments = BaseInfo.TaskObj.UserObj.EDSObj.CityName; // GlobalInfo.Instance.ConfigParam.ProjectNames[0].CityName; //地市--可有可无---毕工确认  @20150808  ---（wjj:根据登录用户获取地市及工程名称）
            RefLteNode.PLMN = new IDType()
            {
                //iid = GlobalInfo.Instance.ConfigParam.ProjectNames[0].ProjectName//工程同名---这个是必须的同---毕工确认  @20150808//
                iid = "plan"//固定值plan---郭琴确认  @20151127//

                //工程名是定死的，用户可选择地市信息，系统根据地市信息获取工程名（可配置）
            };
            RefLteNode.Name2 = BaseInfo.PlanLevel.ToString();//填基站的规划期数；规划期数由用户在界面上输入。
            //RefLteNode.Name2 = BaseInfo.PlanPrjID.ToString();//填基站的规划期数；规划期数由用户在界面上输入。
                                                             //RefLteNode.NodeID = 0;//毕工确认  @20150808 //wjj20150930 可忽略的值不用输入，序列化时会忽略

            #endregion
        }

        /// <summary>
        /// 注入载波信息至RefLteNode
        /// </summary>
        /// <param name="Sectors">用户输入的扇区信息，包括扇区编号，以及扇区天线信息</param>
        /// <param name="RefLteNode">新生成的，EDS中的基站信息</param>
        private static void BuildCarrierInfo(List<CellSector> Sectors, ref LTENodeType RefLteNode)
        {
            List<LTENodeCarrierType> _LteNodeCarriers = new List<LTENodeCarrierType>();
            foreach (var Sector in Sectors)
            {
                List<AirComAntennaType> myAntenna = new List<AirComAntennaType>();
                myAntenna = Sector.Antenners as List<AirComAntennaType>;
                if (myAntenna!=null && myAntenna.Count>0)
                {
                    foreach (var obj in myAntenna)
                    {
                        //var carrier =
                        //    GlobalInfo.Instance.ConfigParam.CarrierItems.Find(Fo => Fo.Alias.Equals(obj.CarrierAlias));//2016-04-03改为由浪潮提供
                        //if (carrier != null)
                        //{
                            #region  获取所有载波信息

                            //LTENodeCarrierType同小区的载波有何区别？
                            //一个小基站可能用几个载波，但是小区只能用其中一个或是多个；
                            LTENodeCarrierType _lteNodeCarrier = new LTENodeCarrierType();
                            _lteNodeCarrier.CarrierID = obj.CarrierId;// carrier.ID; //这个值从里获取,是扇区编号？   2016-04-03改为由浪潮提供
                            if (_LteNodeCarriers.Find(Fo => Fo.CarrierID == obj.CarrierId) == null)
                            {
                                _LteNodeCarriers.Add(_lteNodeCarrier);
                            }

                            #endregion

                        //}
                    }

                }
                RefLteNode.Carriers = _LteNodeCarriers.ToArray();

            }
        }

        private static void BuildCarrierInfo(Dictionary<int, List<EtAntennalnfo>> Sectors, ref LTENodeType RefLteNode)
        {

        }

        /// <summary>
        /// 注入天线信息
        /// </summary>
        /// <param name="Antennas">天线信息</param>
        /// <param name="RefLteNode">新生成的，EDS中的基站信息</param>
        private static void BuildAntennaInfo(List<AirComAntennaType> Antennas, ref LTENodeType RefLteNode)
        {
            #region Antennas
            List<LTEAntennaType> _Antennas = new List<LTEAntennaType>();
            for (int index = 0; index < Antennas.Count; index++)
            {
                AirComAntennaType Item = Antennas[index];
                LTEAntennaType _Antenna = new LTEAntennaType();
                _Antenna.Index = index + 1;//可以采用序列的索引吗？可以
                #region AbsLocation　// 无异议

                _Antenna.AbsLocation = new LocationTypeType()
                {
                    Longitude = Item.Lng,
                    LongitudeSpecified = true,
                    Latitude = Item.Lat,
                    LatitudeSpecified = true

                };
                #endregion

                _Antenna.Azimuth = Item.Azimuth;
                _Antenna.AzimuthSpecified = true;
                _Antenna.ElectricalDownTilt_RO = Item.ElectricalDownTilt;//界面输入的是物理的？还是电子的？//无用，填0 @20150825确认
                _Antenna.ElectricalDownTilt_ROSpecified = true;
                _Antenna.Height = Item.Height;
                _Antenna.HeightSpecified = true;
                //_Antenna.iid = Item　//示例XML中没有
                _Antenna.MechanicalDownTiltSpecified = true;
                _Antenna.MechanicalDownTilt = Item.MechanicalDownTilt;

                #region AntennaPatternType
                //_Antenna.AntennaPatternType = new AntennaPatternType()
                //{
                //    iid = Antennas[index].AntennaTypeName   //就是天线型号  Default_D,Defaut_F

                //};  //这个值从哪里获取？


                _Antenna.AntennaPatternType = new IDType()
                {
                    iid = Antennas[index].AntennaTypeName
                };
                #endregion

                #region PrimaryModel,天线参数中体现
                _Antenna.PrimaryModel = new PredictionModelReferenceType()
                {
                    ModelType = new IDType()
                    {
                        iid = Antennas[index].ModelType //从哪里获取？传播模型由用户在下拉框中选择；配置文件中可配置选项；
                    },
                    RadiusKM = Antennas[index].RadiusKm,//是覆盖半径吗？1，2，3，5，由用户下拉框中选择，默认为2;
                    RadiusKMSpecified = true,
                    ResolutionMetres = Antennas[index].ResolutionMetres,//计算精度，以米为单位，默认为10M；用户可选；扇区添加天线时，需选择计算精度5，10，20
                    ResolutionMetresSpecified = true
                };

                #endregion

                #region Feeders

                List<LTECellFeederType> _LteCellFeeders = new List<LTECellFeederType>();


                LTECellFeederType _fe = new LTECellFeederType();
                //以下参数坐哪里获取？
                _fe.FeederType = new IDType()
                {
                    iid = "Unknown"
                };
                //_fe.LTECellID = (index + 1).ToString();//基站iid-天线索引号（_Antenna.Index）
                _fe.LTECellID = Antennas[index].SectorId.ToString();//扇区ＩＤ,20151126郭琴确认
                _fe.Length = 0;
                _fe.LengthSpecified = true;
                _fe.DLGain = 0;
                _fe.DLGainSpecified = true;
                _fe.MHAGain = 0;
                _fe.MHAGainSpecified = true;
                _fe.MHAType = new IDType()
                {
                    iid = "Unknown"
                };
                _fe.ULGain = 0;
                _fe.ULGainSpecified = true;
                _fe.Noise = 0;
                _fe.NoiseSpecified = true;
                _fe.OtherLosses = 0;
                _fe.OtherLossesSpecified = true;
                _LteCellFeeders.Add(_fe);
                _Antenna.Feeders = _LteCellFeeders.ToArray();

                #endregion
                _Antennas.Add(_Antenna);
            }
            RefLteNode.Antennas = _Antennas.ToArray();

            #endregion

        }
        /// <summary>
        /// 注入小区信息
        /// </summary>
        /// <param name="Sectors">扇区信息，扇区编号和天线构成</param>
        /// <param name="RefLteNode">新生成的，EDS中的基站信息</param>
        private static void BuildCellInfo(List<CellSector> Sectors, ref LTENodeType RefLteNode)
        {
            List<LTECellType> _LteCells = new List<LTECellType>();
            foreach (var Sector in Sectors)
            {
                #region Cells

                var key = (int)Sector.CellID ;
                List<AirComAntennaType> Antennas = Sector.Antenners as List<AirComAntennaType>;
                if (Antennas!=null && Antennas.Count>0)
                {

                    foreach (var obj in Antennas)
                    {

                        LTECellType _LteCell = new LTECellType();
                        //_LteCell.bvid = RefLteNode.bvid;
                        _LteCell.iid = key.ToString(); //添加扇区界面中，用户输入的扇区名称
                        _LteCell.Parent = new IDType()
                        {
                            iid = RefLteNode.iid //LteNode的ＩＩＤ一致　
                        };
                        _LteCell.Tac = 0; //路由编码，采用默认值0
                        _LteCell.TacSpecified = true;
                        _LteCell.LTECellIDSpecified = true;
                        _LteCell.LTECellID =key ;//2015-11-27郭琴确认是扇区ＩＤ //LteCell集合项的索引，采用索引，不重要
                        double max = 31 + ((obj.Power - 0.21) / 3 + (obj.Power - 0.21) % 3 > 0 ? 1 : 0) * 3;
                        _LteCell.Carrier = new LTECellCarrierType()
                        {
                            CarrierID =obj.CarrierId.ToString(),
                            MaxTxPower = max, //最大功能，由用户从界面输入的RS功率换算；换算方法：稍后发邮;
                            MaxTaSpecified = true,
                            NoiseFigure = 6, //采用默认值6
                            NoiseFigureSpecified = true,
                            PhysicalCellID = 0, //采用默认值0
                            PhysicalCellIDSpecified = true

                        };


                        _LteCells.Add((_LteCell));
                    }
                }

                #endregion
            }

            RefLteNode.Cells = _LteCells.ToArray();



        } 

        #endregion

    }
}
