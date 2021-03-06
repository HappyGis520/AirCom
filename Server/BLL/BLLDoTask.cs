﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading;
using JLIB.CSharp;
using JLIB.Utility;
using NetPlan.Model;
using Oracle.ManagedDataAccess.Client;

namespace NetPlan.BLL
{
     public  class BLLDoTask:Singleton<BLLDoTask>
     {
         private AutoResetEvent _ReSet = new AutoResetEvent(false);
        private     Queue<PLAData> PLADatas = new Queue<PLAData>();
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

         private void RegistEAWSAck()
         {

             
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
                     XmlFilePackageInfo xmlFilePage = new XmlFilePackageInfo();
                     //生成XML文件
                     JLog.Instance.AppInfo("开始生成XML文件....");
                     var ok = LteNodeBuildFactory.BuilLTEXMLFilesInterface(_CurProcData.BaseInfo, _CurProcData.CellSectors, _CurProcData.Savedir,
                         out xmlFilePage);
                     if (ok) //生成成功
                     {
                        #region 导入仿真
                        JLog.Instance.AppInfo("开始生成XML成功,开始导入XML文件...");
                        //导入XML文件
                        if (ExecuteCommand(AutoEDSInputCommand(xmlFilePage.InputLTENodeFileFullName, _CurProcData.ProjectName),
                            60000))
                        {
                            JLog.Instance.AppInfo("导入XML文件执行完成,判断导入是否成功...");
                            if (InputXmlSuccess(_CurProcData.BaseInfo.StationAlias, _CurProcData.ProjectName)) //判断导入是否成功，导入成功执行
                            {
                                JLog.Instance.AppInfo("导入XML文件执行成功,启动EAWS仿真...");
                                string Taskname = GetTaskName(_CurProcData.ProjectName);
                                string SchemaName = GlobalInfo.Instance.ConfigParam.EAWSSchemaName;
                                if (!String.IsNullOrEmpty(Taskname))
                                {
                                    BLLEAWS.Instance.UpdateRegionREQ(_CurProcData.RegionBound, SchemaName, Taskname);
                                }
                                else
                                {
                                    JLog.Instance.AppInfo("配置文件中没有找到相应的工程信息，中断");
                                }
                                
                                //if ((_CurProcData.ProjectName)) //启动EAWS仿真，源码有，没有合并
                                {
                                    //    while (true) //等待EAW执行完成
                                    //    {
                                    //        Thread.Sleep(2000);
                                    //    }

                                    //    #region 调用浪潮接口通知


                                    //        #endregion

                                    //    if (Data.BaseInfo.SaveType == EnumSaveType.Delete) //需要删除基站的，执行删除程序
                                    //    {
                                    //        JLog.Instance.AppInfo("执行删除xml操作");

                                    //        #region 执行删除xml操作

                                    //        if (
                                    //            !ExecuteCommand(
                                    //                AutoEDSDeleteCommand(xmlFilePage.DeleteLTENodeFileFullName,
                                    //                    Data.ProjectName),
                                    //                60000))
                                    //        {
                                    //            //提示删除失败
                                    //            JLog.Instance.AppInfo("执行删除xml操作失败");

                                    //        }
                                    //        else
                                    //        {
                                    //            JLog.Instance.AppInfo("执行删除xml操作成功");
                                    //            //提示删除成功
                                    //        }

                                    //        #endregion
                                    //    }

                                }

                            }
                            else
                            {
                                JLog.Instance.AppInfo("导入XML文件执行失败，不执行仿真任务");
                            }
                        } 
                        #endregion
                    }
                     else
                     {
                         //删除ＸＭＬ
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
        /// <summary>
        /// 启动仿真
        /// </summary>
        /// <returns></returns>
         private bool StartEAWS(string ProjectName,string SchemaName)
        {
            try
            {
                string TaskName = GetTaskName(ProjectName);
                if (!string.IsNullOrEmpty(TaskName))
                {
                    return BLLEAWS.Instance.StartTaskREQ(SchemaName, TaskName);
                }
                else
                {
                    JLog.Instance.AppInfo("配置文件中没有找到相应的工程信息，中断");
                    return false;
                }
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                return false;

            }


        }

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
             var cmd = string.Format("{0} -{1} -{2}=\"{3}\" -bvid=\"{4}\"", "aircom.eds.loader.exe", "Create", "input",XmlFileName,ProjectName);
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
            //System.Diagnostics.Process.Start(GlobalInfo.Instance.ConfigParam.EDSLoadAppFile).WaitForExit()；

            System.Diagnostics.ProcessStartInfo myStartInfo = new System.Diagnostics.ProcessStartInfo();
            myStartInfo.FileName = string.Format("{0} ", GlobalInfo.Instance.ConfigParam.EDSLoadAppFile);
            myStartInfo.Arguments = Command;
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo = myStartInfo;
            myProcess.Start();
            myProcess.WaitForExit(WaitForTime); //等待程序退出 
             return true;
         }
    }
}
