using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using JLIB.Utility;

using EAWSProxy.ProxyEAWS;
using NetPlan.Model;

namespace NetPlan.BLL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //This class implements EAWSCallback interface and gets passed to the EAWS Service
    //and is used to receive any "duplex" call back messages from the EAWS Service
    //The class has a MainWindow member variable which gets set inside the constructor
    //to hold a reference to the Test Client.  
    public class EAWSCallBackSolid : EAWSCallback
    {
        private System.Object lockThis = new System.Object();
        public EAWSCallBackSolid()
        {
        }

        //The function is called by the EAWS Service. This in turn calls the Test Client SendUpdate() function
        public void SendUpdate(JobResponseType resp)
        {
            try
            {
                lock (lockThis)
                {
                    if (resp == null)
                    {
                        JLog.Instance.AppInfo("服务端返回空信息");
                        //IDC_RESULT_TEXT.Text += "\n Service Update Failure!!";
                        return;
                    }
                    JLog.Instance.Info("当前所有任务列表如下：");
                    foreach (var key in GlobalInfo.Instance.JobsRunning.Keys)
                    {
                        JLog.Instance.Info(string.Format("key:{0}",key));
                    }
                    


                    if (GlobalInfo.Instance.JobsRunning.ContainsKey(resp.itemIDRef) == false)
                    {
                        JLog.Instance.AppInfo( string.Format( "服务端返回信息,iD={0}没有找到对应的请求", resp.itemIDRef));
                        //IDC_RESULT_TEXT.Text += "\n Service Update Failure. Task GUID not found!!";
                        //IDC_RESULT_TEXT.Text += resp.Status.comment; //Might be exception thrown.
                        return;
                    }

                    //Poll status or Completed Job Update
                    if (resp is TaskCompletionResponse)
                    {
                        JLog.Instance.AppInfo("EAWS服务返回:任务执行完成");
                        #region TaskCompletionResponse
                        TaskCompletionResponse rTaskCompletionRepsonse = resp as TaskCompletionResponse;
                        
                        //Call to RequestTaskStatus() was successful when:
                        //rTaskStatusResponse is not NULL
                        //rTaskStatusResponse is marked with success and
                        //GUID of rTaskStatusResponse and rTaskStatusRequest matches 
                        if (rTaskCompletionRepsonse.Success)
                        {
                            JLog.Instance.AppInfo("任务执行完成");
                            JLog.Instance.AppInfo("任务完成应答，从EAWS请求列表中移除任务");
                            //GlobalInfo.Instance.JobsRunning.Remove(resp.itemIDRef);

                            if (rTaskCompletionRepsonse.OutputLocation.Length > 0)
                            {
                                JLog.Instance.AppInfo("任务执行完成,输出路径: " + rTaskCompletionRepsonse.OutputLocation);
                            }
                            if (rTaskCompletionRepsonse.Finished == true)
                            {
                                JLog.Instance.AppInfo(string.Format("任务:{0} GUID:{1} 的已结束...", rTaskCompletionRepsonse.TaskName, rTaskCompletionRepsonse.itemIDRef.ToString()));
                                //IDC_RESULT_TEXT.Text += "\n Task: " + rTaskCompletionRepsonse.TaskName
                                //       + " Master Guid: " + resp.masterIDRef.ToString()
                                //       + " Guid: " + resp.itemIDRef.ToString() + " is Complete."
                                //       + "\n" + resp.Status.comment;

                            }
                            else
                            {
                                //IDC_RESULT_TEXT.Text += "\n Task: " + rTaskCompletionRepsonse.TaskName
                                //    + " Master Guid: " + resp.masterIDRef.ToString()
                                //    + " Guid: " + resp.itemIDRef.ToString() + " is Running.";
                                JLog.Instance.AppInfo(string.Format("任务:{0} GUID:{1} 的正在运行...", rTaskCompletionRepsonse.TaskName, rTaskCompletionRepsonse.itemIDRef.ToString()));
                            }
                        }
                        else
                        {
                            //IDC_RESULT_TEXT.Text += "\n Unable to get status update for Task: "
                            //+ rTaskCompletionRepsonse.TaskName + " Guid: " + rTaskCompletionRepsonse.itemIDRef.ToString();
                            JLog.Instance.AppInfo(  string.Format( "无法跟踪任务:{0} GUID:{1} 的状态", rTaskCompletionRepsonse.TaskName, rTaskCompletionRepsonse.itemIDRef.ToString()));
                        } 
                        #endregion
                    }
                    else if (resp is StartTaskResponse)
                    {
                        JLog.Instance.AppInfo("EAWS服务返回任务启动结果:");
                        #region StartTaskResponse
                        StartTaskResponse rStartTaskResponse = resp as StartTaskResponse;

                        ////Call to RequestStartTask() was successful when:
                        ////rStartTaskResponse is marked with success
                        if (rStartTaskResponse.Success)
                        {
                            RaiseEAWSTaskStartStateEvent(true,string.Empty);
                            //IDC_RESULT_TEXT.Text += "\n Task: " + rStartTaskResponse.TaskName
                            //    + " Master Guid: " + resp.masterIDRef.ToString()
                            //    + " Guid: " + rStartTaskResponse.itemIDRef.ToString()
                            //    + " " + rStartTaskResponse.Status.comment;
                        }
                        else
                        {
                            string msg = string.Format("无法启动EAWS任务");
                            RaiseEAWSTaskStartStateEvent(false, msg);
                            //IDC_RESULT_TEXT.Text += "\n Unable to start Task: "
                            //    + " Master Guid: " + resp.masterIDRef.ToString()
                            //    + rStartTaskResponse.TaskName + " Guid: " + rStartTaskResponse.itemIDRef.ToString();
                        }
                        #endregion
                    }
                    else if (resp is StopTaskResponse)
                    {
                        JLog.Instance.AppInfo("EAWS服务返回停止任务结果:");
                        #region StopTaskResponse
                        //StopTaskResponse rStopTaskResponse = resp as StopTaskResponse;

                        ////Call to RequestStopTask() was successful when:
                        ////rStartTaskResponse is marked with success
                        //if (rStopTaskResponse.Success)
                        //{
                        //    IDC_RESULT_TEXT.Text += "\n Task: " + rStopTaskResponse.TaskName
                        //        + " Master Guid: " + resp.masterIDRef.ToString()
                        //        + " Guid: " + rStopTaskResponse.itemIDRef.ToString()
                        //        + " " + rStopTaskResponse.Status.comment;
                        //}
                        //else
                        //{
                        //    IDC_RESULT_TEXT.Text += "\n Unable to stop Task: "
                        //        + " Master Guid: " + resp.masterIDRef.ToString()
                        //        + rStopTaskResponse.TaskName + " Guid: " + rStopTaskResponse.itemIDRef.ToString();
                        //} 
                        #endregion
                    }
                    else if (resp is DeleteTaskResponse)
                    {
                        JLog.Instance.AppInfo("EAWS服务返回删除任务结果:");
                        #region DeleteTaskResponse
                        //JLog.Instance.AppInfo("EAWS服务返回删除任务结果:");
                        //DeleteTaskResponse rDeleteTaskResponse = resp as DeleteTaskResponse;

                        ////Call to RequestDeleteTask() was successful when:
                        ////rDeleteTaskResponse is marked with success
                        //if (rDeleteTaskResponse.Success)
                        //{
                        //    IDC_RESULT_TEXT.Text += "\n Task: " + rDeleteTaskResponse.TaskName
                        //        + " Master Guid: " + resp.masterIDRef.ToString()
                        //        + " Guid: " + rDeleteTaskResponse.itemIDRef.ToString()
                        //        + " " + rDeleteTaskResponse.Status.comment;
                        //}
                        //else
                        //{
                        //    IDC_RESULT_TEXT.Text += "\n Unable to delete Task: "
                        //        + " Master Guid: " + resp.masterIDRef.ToString()
                        //        + rDeleteTaskResponse.TaskName + " Guid: " + rDeleteTaskResponse.itemIDRef.ToString();
                        //} 
                        #endregion
                    }
                    else if (resp is TaskStatusResponse)
                    {
                        JLog.Instance.AppInfo("EAWS服务返回任务状态查询结果:");
                        #region TaskStatusResponse
                        //JLog.Instance.AppInfo("EAWS服务返回任务状态:");
                        //TaskStatusResponse rTaskStatusResponse = resp as TaskStatusResponse;

                        ////Call to RequestTaskStatus() was successful when:
                        ////rTaskStatusResponse is not NULL
                        ////rTaskStatusResponse is marked with success and
                        ////GUID of rTaskStatusResponse and rTaskStatusRequest matches 
                        //if (rTaskStatusResponse.Success)
                        //{
                        //    m_JobsRunning.Remove(resp.itemIDRef);
                        //    IDC_RESULT_TEXT.Text += "\n Task: " + rTaskStatusResponse.TaskName
                        //        + " Master Guid: " + resp.masterIDRef.ToString()
                        //        + " Guid: " + rTaskStatusResponse.itemIDRef.ToString();

                        //    //Access the Number of warnings, Errors, Outstanding pieces of work and
                        //    //Merge activities of running task.
                        //    //If Outstanding pieces of work is zero then task is finished
                        //    IDC_RESULT_TEXT.Text += "\n Succeeded: " + rTaskStatusResponse.NumSucceeded.ToString()
                        //        + " Warnings: " + rTaskStatusResponse.NumWarning.ToString()
                        //        + " Errors: " + rTaskStatusResponse.NumError.ToString()
                        //        + " Outstanding: " + rTaskStatusResponse.NumOutStanding.ToString()
                        //        + " To Merge: " + rTaskStatusResponse.NumAwaitingMerge.ToString()
                        //        + rTaskStatusResponse.Status.comment;
                        //}
                        //else
                        //{
                        //    IDC_RESULT_TEXT.Text += "\n Unable to get status update for Task: "
                        //        + " Master Guid: " + resp.masterIDRef.ToString()
                        //        + rTaskStatusResponse.TaskName + " Guid: " + rTaskStatusResponse.itemIDRef.ToString();
                        //} 
                        #endregion
                    }
                    else if (resp is EditTaskRegionResponse)
                    {
                        JLog.Instance.AppInfo("EAWS服务返回编辑仿真范围结果");
                        #region EditTaskRegionResponse
                        EditTaskRegionResponse rEditTaskRegionResponse = resp as EditTaskRegionResponse;
                        if (rEditTaskRegionResponse.Success)
                        {
                            JLog.Instance.AppInfo("编辑仿真范围成功");
                            //EAWSBackEvent.Instance.SubdoEditRegionAck(true);
                            RaiseEditRegionAckEvent(true, string.Empty);
                        }
                        else
                        {

                            JLog.Instance.AppInfo("编辑仿真范围失败");

                            //EAWSBackEvent.Instance.SubdoEditRegionAck(false);
                            RaiseEditRegionAckEvent(false, "编辑仿真范围失败");

                        } 
                        #endregion
                    }
                    else if (resp is EditTaskFiltersResponse)
                    {
                        JLog.Instance.AppInfo("EAWS服务返回任务过滤结果:");
                        #region EditTaskFiltersResponse
                        //EditTaskFiltersResponse rEditTaskFiltersResponse = resp as EditTaskFiltersResponse;

                        ////Call to RequestEditTaskFilters() was successful when:
                        ////rEditTaskFiltersResponse is marked with success 
                        //if (rEditTaskFiltersResponse.Success)
                        //{
                        //    IDC_RESULT_TEXT.Text += "\n Task: " + rEditTaskFiltersResponse.TaskName
                        //        + " Master Guid: " + resp.masterIDRef.ToString()
                        //        + " Guid: " + rEditTaskFiltersResponse.itemIDRef.ToString()
                        //        + " " + rEditTaskFiltersResponse.Status.comment;
                        //}
                        //else
                        //{
                        //    IDC_RESULT_TEXT.Text += "\n Unable to edit region for Task: "
                        //        + " Master Guid: " + resp.masterIDRef.ToString()
                        //        + rEditTaskFiltersResponse.TaskName + " Guid: " + rEditTaskFiltersResponse.itemIDRef.ToString();
                        //} 
                        #endregion
                    }
                    else if (resp is AllSchemaNamesResponse)
                    {
                        JLog.Instance.AppInfo("EAWS服务返回获取Schema结果:");
                        #region AllSchemaNamesResponse
                        ////Call RequestAllSchemaNames passing rAllSchemaNamesRequest as a parameter
                        ////It gets a AllSchemaNamesResponse as a return parameter
                        //AllSchemaNamesResponse rAllSchemaNamesResponse = resp as AllSchemaNamesResponse;

                        ////Call to RequestAllSchemaNames() was successful when:
                        ////rAllSchemaNamesResponse is marked with success and
                        ////rAllSchemaNamesResponse.AllSchemaNames is not empty as this holds list of all schemas
                        //if (rAllSchemaNamesResponse.Success && rAllSchemaNamesResponse.AllSchemaNames != null)
                        //{
                        //    //Extract each schema from rAllSchemaNamesResponse.AllSchemaNames
                        //    this.IDC_SCHEMA_COMBO.Items.Clear();
                        //    foreach (string taskname in rAllSchemaNamesResponse.AllSchemaNames)
                        //    {
                        //        this.IDC_SCHEMA_COMBO.Items.Add(taskname);
                        //    }
                        //}
                        //else
                        //{
                        //    IDC_RESULT_TEXT.Text += "\n Unable to get list of schemas.";
                        //} 
                        #endregion
                    }
                    else if (resp is AllSchemaTaskNamesResponse)
                    {
                        JLog.Instance.AppInfo("EAWS服务返回:");
                        #region AllSchemaTaskNamesResponse
                        //AllSchemaTaskNamesResponse rAllSchemaTaskNamesResponse = resp as AllSchemaTaskNamesResponse;

                        ////Call to RequestAllTaskNames() was successful when:
                        ////rAllSchemaTaskNamesResponse is marked with success and
                        ////rAllSchemaTaskNamesResponse.AllTaskNames is not empty as this holds list of all tasks
                        //if (rAllSchemaTaskNamesResponse.Success && rAllSchemaTaskNamesResponse.AllTaskNames != null)
                        //{
                        //    //Extract each task from rAllSchemaNamesResponse.AllTaskNames
                        //    this.IDC_TASK_COMBO.Items.Clear();
                        //    foreach (string taskname in rAllSchemaTaskNamesResponse.AllTaskNames)
                        //    {
                        //        this.IDC_TASK_COMBO.Items.Add(taskname);
                        //    }
                        //}
                        //else
                        //{
                        //    IDC_RESULT_TEXT.Text += "\n Unable to get list of tasks for schema: "
                        //        + rAllSchemaTaskNamesResponse.SchemaName;
                        //} 
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                    JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                        MethodBase.GetCurrentMethod().Module.Name);
            }
        }



        #region 自定义事件

        #region 编辑仿真范围应答

        protected  Action<bool,string> EditRegionAckEvent;

        //protected void SubDoEditRegionAck(bool Success, string Msg)
        //{
        //    RaiseEditRegionAckEvent( Success,  Msg);
        ////}

        protected void RaiseEditRegionAckEvent(bool Success, string Msg)
        {
            try
            {
                JLog.Instance.AppInfo("EAWS回调函数产生事件EditRegionAckEvent");
                if (EditRegionAckEvent != null)
                {
                    EditRegionAckEvent.BeginInvoke(Success, Msg, null, null);
                }
                else
                {
                    JLog.Instance.AppInfo("事件EditRegionAckEvent没有回调方法");
                }

            }
            catch (DbException ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                Msg = ex.Message;
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                Msg = ex.Message;
            }
            


        }

        public void RegistEditRegionAckEvent(Action<bool,string> handle)
        {
            JLog.Instance.AppInfo("注册EditRegionAckEvent事件方法被调用");
            try
            {
                DeRegistEditRegionAckEvent(handle);
                EditRegionAckEvent = handle;

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

        public void DeRegistEditRegionAckEvent(Action<bool,string> handle)
        {
            EditRegionAckEvent = null;

        }

        #endregion



        #region EAWS仿真任务启动状态

        protected  Action<bool,string> EAWSTaskStartStateEvent;

        protected void SubDoEAWSTaskStartState(bool Success,string Msg)
        {
            RaiseEAWSTaskStartStateEvent(Success,Msg);
        }

        protected void RaiseEAWSTaskStartStateEvent(bool Success, string Msg)
        {
            try
            {
                JLog.Instance.AppInfo("EAWS回调函数产生事件EAWSTaskStartStateEvent");
                if (EAWSTaskStartStateEvent != null)
                {
                    EAWSTaskStartStateEvent.BeginInvoke(Success, Msg, null, null);
                }
                else
                {
                    JLog.Instance.AppInfo("事件EAWSTaskStartStateEvent没有回调方法");
                }
            }
            catch (DbException ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                Msg = ex.Message;
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                Msg = ex.Message;
            }

        }

        public void RegistEAWSTaskStartStateEvent(Action<bool, string > handle)
        {
            JLog.Instance.AppInfo("注册EAWSTaskStartStateEvent事件方法被调用");
            DeRegistEAWSTaskStartStateEvent(handle);
            EAWSTaskStartStateEvent = handle;


        }

        public void DeRegistEAWSTaskStartStateEvent(Action<bool,string> handle)
        {
            EAWSTaskStartStateEvent = null;

        }

        #endregion



        #region 仿真任务执行结果应答

        protected  Action<bool,string> EAWSTaskCompletAckEvent;

        protected void SubDoEAWSTaskCompletAck(bool Success , string SavePath)
        {
            RaiseEAWSTaskCompletAckEvent( Success,  SavePath);
        }

        protected void RaiseEAWSTaskCompletAckEvent(bool Success, string SavePath)
        {
            try
            {
                JLog.Instance.AppInfo("EAWS回调函数产生事件EAWSTaskCompletAckEvent");
                if (EAWSTaskCompletAckEvent != null)
                {
                    EAWSTaskCompletAckEvent.BeginInvoke(Success, SavePath, null, null);

                }
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

        public void RegistEAWSTaskCompletAckEvent(Action<bool, string> handle)
        {
            JLog.Instance.AppInfo("注册EAWSTaskCompletAckEvent事件方法被调用");
            DeRegistEAWSTaskCompletAckEvent(handle);
            EAWSTaskCompletAckEvent = handle;


        }

        public void DeRegistEAWSTaskCompletAckEvent(Action<bool, string> handle)
        {
            EAWSTaskCompletAckEvent = null;

        }

        #endregion





        #endregion

    }
}
