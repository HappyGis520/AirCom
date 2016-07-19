using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using JLIB.CSharp;
using JLIB.Utility;
using NetPlan.Model;
using Oracle.ManagedDataAccess.Client;
using ZipOneCode.ZipProvider;
namespace NetPlan.BLL
{
     public  class BLLDoTask:Singleton<BLLDoTask>
     {
         BLLEAWS _bllEAWs = null;
        private AutoResetEvent _ReSet = new AutoResetEvent(false);
        private  Queue<PLAData> PLADatas = new Queue<PLAData>();
         private string EAWSReleaseSaveDir = string.Empty;
        /// <summary>
        /// 是否执行下一步
        /// </summary>
         private bool DoNextTask = false;

        private void CreateNewEAWSHandle()
        {
            _bllEAWs = new BLLEAWS();
            _bllEAWs.RegistEditRegionAckEvent(SubDoEditRegionAck);
            _bllEAWs.RegistEAWSTaskStartStateEvent(SubDoEAWSTaskStartState);
            _bllEAWs.RegistEAWSTaskCompletAckEvent(SubDoEAWSTaskCompletAck);

        }

        public void CreateTask(PLAData Data)
         {
            try
            {
                JLog.Instance.AppInfo("外部程序调用，添加仿真数据");
                Monitor.Enter(PLADatas);
                Data.Savedir = GlobalInfo.Instance.ConfigParam.XmlFileSaveDir;
                JLog.Instance.AppInfo(string.Format("保存路径{0}",Data.Savedir));
                PLADatas.Enqueue(Data);
                JLog.Instance.AppInfo(string.Format("添加到仿真数据队列，当前共计{0}", PLADatas.Count));
                Monitor.Exit(PLADatas);
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message);
            }

             
         }

         Thread DoThread = null;
        /// <summary>
        /// 当前处理的数据
        /// </summary>
        PLAData _CurProcData = null;
        public BLLDoTask()
         {
             try
             {
                JLog.Instance.AppInfo("开始创建任务，执行一系列操作");
                DoThread = new Thread(DoWork);
                DoThread.Start();
            }
             catch (Exception ex)
             {
                 JLog.Instance.Error(ex.Message);
             }

        }


         private void DoWork()
         {
            JLog.Instance.AppInfo("仿真线程已启动.....");
             while (true)
             {

                #region 提取仿真数据
                Monitor.Enter(PLADatas);

                if (PLADatas.Count >= 1)
                {
                    _CurProcData = PLADatas.Dequeue();
                }
                else
                {
                    _CurProcData = null;
                }
                Monitor.Exit(PLADatas); 
                #endregion
                if (_CurProcData != null)
                 {
                    CreateNewEAWSHandle();
                    XmlFilePackageInfo xmlFilePage = new XmlFilePackageInfo();
                     //生成XML文件
                     JLog.Instance.AppInfo("开始生成XML文件....");
                     var _BuilLTEXMLResult = LteNodeBuildFactory.BuilLTEXMLFilesInterface(_CurProcData.BaseInfo, _CurProcData.CellSectors, _CurProcData.Savedir,
                         out xmlFilePage);
                     if (_BuilLTEXMLResult) //生成成功
                     {
                        #region 导入XML文件
                        JLog.Instance.AppInfo("开始生成XML成功,开始...");
                        //导入XML文件
                        if (ExecuteCommand(AutoEDSInputCommand(xmlFilePage.InputLocationFileFullName, _CurProcData.ProjectName),
                            60000))
                        {
                            //JLog.Instance.AppInfo("导入XML文件执行完成,判断导入位置数据是否成功...");
                            //if (InputXmlSuccess(_CurProcData.BaseInfo.StationAlias, _CurProcData.ProjectName)) //判断导入是否成功，导入成功执行
                            //{
                                if (
                                    ExecuteCommand(AutoEDSInputCommand(xmlFilePage.InputLTENodeFileFullName,_CurProcData.ProjectName),
                                        60000))
                                {
                                    JLog.Instance.AppInfo("导入XML文件执行完成,判断导入位置数据是否成功...");
                                    if (InputXmlSuccess(_CurProcData.BaseInfo.StationId, _CurProcData.ProjectName))
                                        //判断导入是否成功，导入成功执行
                                    {
                                        JLog.Instance.AppInfo("导入XML文件执行成功,启动EAWS仿真...");

                                        #region 启动EAWS仿真

                                        string Taskname = GetTaskName(_CurProcData.ProjectName);
                                        string SchemaName = GlobalInfo.Instance.ConfigParam.EAWSSchemaName;
                                        if (!String.IsNullOrEmpty(Taskname))
                                        {
                                            //获取带号

                                            var ProjectInfo = GlobalInfo.Instance.ConfigParam.ProjectNames
                                                .FirstOrDefault(
                                                    Fo => Fo.ProjectName == _CurProcData.ProjectName);
                                            if (ProjectInfo == null)
                                            {

                                                JLog.Instance.AppInfo(string.Format("配置文件中没有找到工程名为：{0}的工程",
                                                    _CurProcData.ProjectName));
                                                continue;
                                            }
                                            var ProjNo = ProjectInfo.UtmID;
                                            JLog.Instance.AppInfo(string.Format("工程投影带号为：{0}", ProjNo));
                                            _CurProcData.GetExtend(_CurProcData.BaseInfo.Lng, _CurProcData.BaseInfo.Lat,
                                                out _CurProcData.RegionBound.EastMin,
                                                out _CurProcData.RegionBound.Eastmax,
                                                out _CurProcData.RegionBound.NorthMin,
                                                out _CurProcData.RegionBound.NorthMax,
                                                _CurProcData.CoverRadius*1000, ProjNo);


                                            //_CurProcData.RegionBound = WGS84ToUTM.LatLonToUTM(
                                            //    _CurProcData.BaseInfo.Lng, _CurProcData.BaseInfo.Lat, ProjNo,
                                            //    _CurProcData.CoverRadius);
                                            JLog.Instance.AppInfo(string.Format("工程坐标范围为：{0}",
                                                _CurProcData.RegionBound.ToString()));
                                            _bllEAWs.UpdateRegionREQ(_CurProcData.RegionBound, SchemaName,
                                                Taskname);
                                            DoNextTask = false;
                                            _ReSet.WaitOne(600000);
                                            JLog.Instance.AppInfo("_ReSet等待完成，执行下一步操作");
                                            if (DoNextTask)
                                            {
                                                JLog.Instance.AppInfo("编辑仿真范围完成");

                                                #region 编辑仿真范围完成

                                                DoNextTask = false;
                                                JLog.Instance.AppInfo("发送启动仿真请求");
                                            _bllEAWs.StartTaskREQ(SchemaName, Taskname);
                                            DoNextTask = false;
                                            _ReSet.WaitOne(60000);
                                            if (DoNextTask)
                                            {
                                                JLog.Instance.AppInfo("启动仿真任务完成,等待任务执行完成");
                                                DoNextTask = false;
                                                _ReSet.WaitOne(3600000);
                                                JLog.Instance.AppInfo("_ReSet等待完成，执行下一步操作");
                                                if (DoNextTask)
                                                {
                                                    JLog.Instance.AppInfo("仿真任务执行完成");

                                                    #region 仿真任务执行完成后操作

                                                    if (string.IsNullOrEmpty(EAWSReleaseSaveDir))
                                                    {
                                                        JLog.Instance.AppInfo(string.Format("EAWS返回仿真结果保存路径：{0}",
                                                            EAWSReleaseSaveDir));
                                                        continue;
                                                    }
                                                    EAWSReleaseSaveDir = string.Format(@"{0}\{1}", EAWSReleaseSaveDir,
                                                        ProjectInfo.ProjectName);
                                                    JLog.Instance.AppInfo(string.Format("仿真结果保存路径：{0}",
                                                        EAWSReleaseSaveDir));
                                                    JLog.Instance.AppInfo("压缩文件，上传至浪潮");
                                                    string rarFileName = string.Format("{0}.rar",
                                                        Guid.NewGuid().ToString());
                                                    ZipHelper.Zip(
                                                        Path.Combine(GlobalInfo.Instance.ConfigParam.FTPDir,
                                                            rarFileName), EAWSReleaseSaveDir, string.Empty,
                                                        ZipHelper.CompressLevel.Level6);
                                                    Inspur.InspurRequestApiModel sendmodel = new Inspur.
                                                        InspurRequestApiModel()
                                                    {
                                                        flow_id = _CurProcData.WorkOrder,
                                                        name = "wjj",
                                                        remark = ""
                                                    };
                                                    var sendxml = XMLHelper.SerializeToXmlStr(sendmodel, true);
                                                    var s = new Inspur.InspurService.TaircomServiceImplService();
                                                    var recxml = s.SycAirCom(sendxml);
                                                    var recModel =
                                                        XMLHelper.XmlDeserialize<Inspur.InspurResponeseApiModel>(
                                                            recxml);
                                                    if (recModel.is_archive.Equals("0"))
                                                    {
                                                        JLog.Instance.AppInfo("浪潮返回调用成功消息");
                                                    }
                                                    else
                                                    {
                                                        JLog.Instance.AppInfo("浪潮返回调用失败消息");
                                                    }
                                                    if (_CurProcData.BaseInfo.SaveType == EnumSaveType.Delete)
                                                        //需要删除基站的，执行删除程序
                                                    {
                                                        JLog.Instance.AppInfo("执行删除xml操作");

                                                        #region 执行删除xml操作

                                                        if (
                                                            !ExecuteCommand(
                                                                AutoEDSDeleteCommand(
                                                                    xmlFilePage.DeleteLTENodeFileFullName,
                                                                    _CurProcData.ProjectName),
                                                                60000))
                                                        {
                                                            //提示删除失败
                                                            JLog.Instance.AppInfo("执行删除xml操作失败");

                                                        }
                                                        else
                                                        {
                                                            JLog.Instance.AppInfo("执行删除xml操作成功");
                                                            //提示删除成功
                                                        }

                                                        #endregion
                                                    }

                                                    #endregion
                                                }
                                                JLog.Instance.Info("仿真任务执行失败,无需执行下一步，退出");
                                        }
                                        JLog.Instance.Info("启动仿真失败，无需执行下一步，退出");

                                        #endregion
                                    }
                                            JLog.Instance.AppInfo("编辑仿真范围失败,无需执行下一步");
                                        

                                    }
                                        else
                                        {
                                            JLog.Instance.AppInfo("配置文件中没有找到相应的工程信息，中断");
                                        }

                                        #endregion

                                        #region MyRegion

                                        //if ((_CurProcData.ProjectName)) //启动EAWS仿真，源码有，没有合并
                                        {
                                            //    while (true) //等待EAW执行完成
                                            //    {
                                            //        Thread.Sleep(2000);
                                            //    }

                                            //    #region 调用浪潮接口通知


                                            //        #endregion




                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        JLog.Instance.AppInfo("导入XML文件执行失败，不执行仿真任务");
                                    }
                                }
                            //}
                            //else
                            //{
                            //    JLog.Instance.AppInfo("导入XML文件执行失败，不执行仿真任务");
                            //}
                        } 
                        #endregion
                    }
                 }
                 else
                 {

                    JLog.Instance.AppInfo("没有提取到任务，等待...");
                    Thread.Sleep(30000);
                }
                JLog.Instance.AppInfo("等待下一次任务执行-----------------");
                Thread.Sleep(10000);
             }


         }
        ///// <summary>
        ///// 启动仿真
        ///// </summary>
        ///// <returns></returns>
        // private bool StartEAWS(string ProjectName,string SchemaName)
        //{
        //    try
        //    {
        //        string TaskName = GetTaskName(ProjectName);
        //        if (!string.IsNullOrEmpty(TaskName))
        //        {
        //            return BLLEAWS.Instance.StartTaskREQ(SchemaName, TaskName);
        //        }
        //        else
        //        {
        //            JLog.Instance.AppInfo("配置文件中没有找到相应的工程信息，中断");
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
        //            MethodBase.GetCurrentMethod().Module.Name);
        //        return false;

        //    }


        //}

         private string GetTaskName(string ProjectName)
         {
             try
             {
                var obj = GlobalInfo.Instance.ConfigParam.ProjectNames.FindAll(Fo => Fo.ProjectName.Equals(ProjectName));
                if (obj != null && obj.Count > 0)
                {
                    return  obj[0].TaskName;

                }
                 return string.Empty;
             }
             catch (Exception ex)
             {
                 JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                     MethodBase.GetCurrentMethod().Module.Name);
                 return string.Empty;


             }
         }

         private string GetCityEName(string ProjectName)
         {
            try
            {
                var obj = GlobalInfo.Instance.ConfigParam.ProjectNames.FindAll(Fo => Fo.ProjectName.Equals(ProjectName));
                if (obj != null && obj.Count > 0)
                {
                    return obj[0].CityEName;

                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                return string.Empty;


            }
        }
       
        
        /// <summary>
        /// 判断导入xml是否成功
        /// </summary>
        /// <param name="StationName">基站名称</param>
        /// <param name="CityPrjName">工程名称</param>
        /// <returns></returns>
        private bool InputXmlSuccess(string StationName, string CityPrjName)
         {
            try
            {
                string connStr = string.Format(
                    "User Id={0};Password={1};Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME={3})))",
                    GlobalInfo.Instance.ConfigParam.EDSDBInfo.UserName, GlobalInfo.Instance.ConfigParam.EDSDBInfo.Password, GlobalInfo.Instance.ConfigParam.EDSDBInfo.Host, GlobalInfo.Instance.ConfigParam.EDSDBInfo.SERVICE_NAME);
                JLog.Instance.AppInfo(connStr);
                using (var conn = new OracleConnection(connStr))
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    var SQLStr = string.Format(
                        "select t1.idname site,t2.idname cell from network_planning.lognode t1, network_planning.logcell t2, network_planning.project t3 " +
                        "where t1.projectno = t2.projectno and t1.projectno = t3.projectnumber and t1.lognodepk = t2.lognodefk and t1.idname = '{0}' and t3.name = '{1}'",
                        StationName,CityPrjName);
                    //string sql = "select * from Location";
                    JLog.Instance.AppInfo(string.Format("执行oracle查询命令：{0}", SQLStr));
                    OracleDataAdapter oda = new OracleDataAdapter(SQLStr, conn);
                    oda.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (OracleException ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                throw new Exception(ex.Message);
            }
        }
         /// <summary>
         /// AutoEds 创建基站命令字符串
         /// </summary>
         /// <param name="XmlFileName"></param>
         /// <param name="ProjectName"></param>
         /// <returns></returns>
         private string AutoEDSInputCommand(string XmlFileName,string ProjectName)
         {
             //var cmd = string.Format("{0} -{1} -{2}=\"{3}\" -bvid=\"{4}\"", "aircom.eds.loader.exe", "create", "input",XmlFileName,ProjectName);
             var cmd = string.Format(" -{1} -{2}=\"{3}\" -bvid=\"{4}\"", "aircom.eds.loader.exe", "create", "input", XmlFileName, ProjectName);
            JLog.Instance.AppInfo(string.Format("执行导入XML命令：{0}",cmd));
            return cmd;
         }
        /// <summary>
        /// AutoEds 删除基站命令字符串
        /// </summary>
        /// <param name="XmlFileName"></param>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        private string AutoEDSDeleteCommand(string XmlFileName, string ProjectName)
        {
            var cmd = string.Format("{0} -{1} -{2}=\"{3}\" -bvid=\"{4}\"", "aircom.eds.loader.exe", "Create", "input", XmlFileName, ProjectName);
            JLog.Instance.AppInfo(string.Format("执行删除XML命令：{0}", cmd));
            return cmd;
        }
        /// <summary>
        /// 执行autoEDS命令字符串
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="WaitForTime"> </param>
        /// <returns></returns>
        private bool ExecuteCommand(string Command,int WaitForTime)
         {
            System.Diagnostics.ProcessStartInfo myStartInfo = new System.Diagnostics.ProcessStartInfo();
            myStartInfo.FileName = string.Format("{0} ", GlobalInfo.Instance.ConfigParam.EDSLoadAppFile);
            myStartInfo.Arguments = Command;
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo = myStartInfo;
            myProcess.Start();
            myProcess.WaitForExit(WaitForTime); //等待程序退出 
             return true;
         }

        #region 自定义事件

        #region 编辑仿真范围应答



        protected void SubDoEditRegionAck(bool Success, string Msg)
        {
            try
            {
                JLog.Instance.AppInfo(string.Format("设置仿真范围{0}", Success ? "成功" : "失败"));
                if (Success)
                {
                    DoNextTask = true;
                }
                _ReSet.Set();
            }
            catch (DbException ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);

            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);

            }


        }



        #endregion



        #region EAWS仿真任务启动状态



        protected void SubDoEAWSTaskStartState(bool Success, string Msg)
        {
            JLog.Instance.AppInfo(string.Format("启动仿真任务{0}", Success ? "成功" : "失败"));
            if (Success)
            {
                DoNextTask = true;
            }
            _ReSet.Set();
        }



        #endregion



        #region 仿真任务执行结果应答



        protected void SubDoEAWSTaskCompletAck(bool Success, string SavePath)
        {
            JLog.Instance.AppInfo(string.Format("运行仿真任务{0}", Success ? "成功" : "失败"));
            if (Success)
            {
                EAWSReleaseSaveDir = SavePath;
                if (string.IsNullOrEmpty(SavePath))
                {
                    JLog.Instance.AppInfo("EAWSIM没有返回输出目录，从配置文件获取输出目录");
                    var ename = GetCityEName(_CurProcData.ProjectName);
                    EAWSReleaseSaveDir = string.Format(GlobalInfo.Instance.ConfigParam.EAWSRealseDir, ename);
                    JLog.Instance.AppInfo(string.Format("从配置文件获取输出目录{0}", EAWSReleaseSaveDir));

                }
                DoNextTask = true;
            }
            _ReSet.Set();
        }


        #endregion





        #endregion
    }
}
