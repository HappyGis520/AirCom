using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.ServiceModel;
using JLIB.Utility;

using EAWSProxy.ProxyEAWS;
using JLIB.CSharp;
using NetPlan.Model;


namespace NetPlan.BLL
{
     public  class BLLEAWS
     {

         private EAWSCallBackSolid _CallBackHandle = null;
         InstanceContext _callbackContext = null;
        private EAWSClient m_EAWSClient;
        private System.Object lockThis = new System.Object();
        private int counter = 0;

        public BLLEAWS()
        {
            _CallBackHandle = new EAWSCallBackSolid();
            RegistEventHandle();
            _callbackContext = new InstanceContext(_CallBackHandle);
            m_EAWSClient = new EAWSClient(_callbackContext, "EAWSTCPService");
            GlobalInfo.Instance.JobsRunning = new Hashtable();
        }

         private void RegistEventHandle()
         {
            JLog.Instance.AppInfo("在ＥＡＷＳ回调方法上注册事件方法　");
            _CallBackHandle.RegistEditRegionAckEvent(SubDoEditRegionAck);
            _CallBackHandle.RegistEAWSTaskStartStateEvent(SubDoEAWSTaskStartState);
            _CallBackHandle.RegistEAWSTaskCompletAckEvent(SubDoEAWSTaskCompletAck);
            
         }
         public void GetAllTasksREQ()
         {
             ////Create a new AllSchemaTaskNamesRequest Object and create a new message GUID
             //AllSchemaTaskNamesRequest rAllSchemaTaskNamesRequest = new AllSchemaTaskNamesRequest();
             //rAllSchemaTaskNamesRequest.itemID = Guid.NewGuid();

             ////Set task name
             //rAllSchemaTaskNamesRequest.SchemaName = "EAWSTCPService";

             ////Store Job GUID for Callback
             //m_JobsRunning[rAllSchemaTaskNamesRequest.itemID] = rAllSchemaTaskNamesRequest;
             //using (EAWSClient _client = new EAWSClient())
             //{
             //    //Call RequestAllTaskNames passing rAllSchemaTaskNamesRequest as a parameter
             //    _client.QueryRequest(rAllSchemaTaskNamesRequest);
             //}

         }
        /// <summary>
        /// 获取SCHEMAS
        /// </summary>
        private void LOAD_SCHEMAS_REQ()
        {
            try
            {
                JLog.Instance.AppInfo("开始加载SCHEMAS.....");
                //Create a new AllSchemaNamesRequest Object and create a new message GUID
                AllSchemaNamesRequest rAllSchemaNamesRequest = new AllSchemaNamesRequest();
                rAllSchemaNamesRequest.itemID = Guid.NewGuid();
                JLog.Instance.AppInfo(string.Format("添加EWAW请求－－加载SCHEMAS任务序号：{0}", rAllSchemaNamesRequest.itemID));
                //Store Job GUID for Callback
                GlobalInfo.Instance.JobsRunning[rAllSchemaNamesRequest.itemID] = rAllSchemaNamesRequest;
                //GlobalInfo.Instance.JobsRunning.Add(rAllSchemaNamesRequest.itemID, rAllSchemaNamesRequest);
                //Call RequestAllSchemaNames passing rAllSchemaNamesRequest as a parameter
                m_EAWSClient.QueryRequest(rAllSchemaNamesRequest);

            }
            catch (Exception ex)
            {
                    JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                        MethodBase.GetCurrentMethod().Module.Name);
            }
        }

        private void LOAD_TASKS_REQ()
        {
            //try
            //{
            //    if (IDC_SCHEMA_COMBO.SelectedItem != null && IDC_SCHEMA_COMBO.SelectedItem.ToString() != "")
            //    {
            //        //Create a new AllSchemaTaskNamesRequest Object and create a new message GUID
            //        EAWSService.AllSchemaTaskNamesRequest rAllSchemaTaskNamesRequest = new AllSchemaTaskNamesRequest();
            //        rAllSchemaTaskNamesRequest.itemID = Guid.NewGuid();

            //        //Set task name
            //        rAllSchemaTaskNamesRequest.SchemaName = IDC_SCHEMA_COMBO.SelectedItem.ToString();

            //        //Store Job GUID for Callback
            //GlobalInfo.Instance.JobsRunning[rAllSchemaTaskNamesRequest.itemID] = rAllSchemaTaskNamesRequest;

            //        //Call RequestAllTaskNames passing rAllSchemaTaskNamesRequest as a parameter
            //        m_EAWSClient.QueryRequest(rAllSchemaTaskNamesRequest);
            //    }
            //    else
            //    {
            //        IDC_RESULT_TEXT.Text += "\n Please Select a Schema";
            //    }
            //}
            //catch (Exception ex)
            //{
            //        JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
            //            MethodBase.GetCurrentMethod().Module.Name);
            //}
        }


        /// <summary>
        /// 编辑范围
        /// </summary>
        /// <param name="Region"></param>
        public void UpdateRegionREQ(GeoRegion Region,string SchemaName,string TaskName)
        {
            try
            {
                //EditRegion rEditRegion = new EditRegion();
                //rEditRegion.ShowDialog();

                //if (rEditRegion.UpdateRegion == true)
                //{
                    //Create a new EditTaskRegionRequest Object and create a new message GUID
                    EditTaskRegionRequest rEditTaskRegionRequest = new EditTaskRegionRequest();
                    rEditTaskRegionRequest.itemID = Guid.NewGuid();

                    //set the schema name, task name and the region.
                    rEditTaskRegionRequest.SchemaName = SchemaName;//IDC_SCHEMA_COMBO.SelectedItem.ToString();
                    rEditTaskRegionRequest.TaskName = TaskName;// IDC_TASK_COMBO.SelectedItem.ToString();
                    rEditTaskRegionRequest.EastMin = Convert.ToInt32(Region.EastMin);// Convert.ToInt32(rEditRegion.IDC_EASTMIN.Text);
                    rEditTaskRegionRequest.EastMax = Convert.ToInt32(Region.Eastmax);// Convert.ToInt32(rEditRegion.IDC_EASTMAX.Text);
                    rEditTaskRegionRequest.NorthMin = Convert.ToInt32(Region.NorthMin);// Convert.ToInt32(rEditRegion.IDC_NORTHMIN.Text);
                    rEditTaskRegionRequest.NorthMax = Convert.ToInt32(Region.NorthMax);// Convert.ToInt32(rEditRegion.IDC_NORTHMAX.Text);
                 JLog.Instance.Info(string.Format( " {0} {1} {2} {3}", rEditTaskRegionRequest.EastMin, rEditTaskRegionRequest.EastMax, rEditTaskRegionRequest.NorthMin, rEditTaskRegionRequest.NorthMax));
                JLog.Instance.AppInfo(string.Format("添加EWAW请求--编辑仿真范围任务序号：{0}", rEditTaskRegionRequest.itemID));
                GlobalInfo.Instance.JobsRunning[rEditTaskRegionRequest.itemID] = rEditTaskRegionRequest;
                //GlobalInfo.Instance.JobsRunning.Add(rEditTaskRegionRequest.itemID, rEditTaskRegionRequest);
                    //Call RequestEditTaskRegion passing rEditTaskRegionRequest as a parameter
                    m_EAWSClient.EditRequest(rEditTaskRegionRequest);
                //}
            }
            catch (Exception ex)
            {
                    JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                        MethodBase.GetCurrentMethod().Module.Name);
            }
        }


        //This function gets called by EAWSCallBackSolid object when it receives a call back from EAWS Service.
        //Any call to the EAWSService will get a response via this call back plus on running poll status
        //and completed job status of any running job will be sent via this call back
        //It receives a EAWSService.JobResponseType which can be tested and cast to determine
        //the required repspnse.
        //public void SendUpdate(JobResponseType resp)
        //{
        //    try
        //    {
        //        String MSG = string.Empty;
        //        lock (lockThis)
        //        {
        //            if (resp == null)
        //            {
        //                JLog.Instance.Error(" Service Update Failure!!");
        //                return;
        //            }

        //            if (GlobalInfo.Instance.JobsRunning.ContainsKey(resp.itemIDRef) == false)
        //            {
        //                JLog.Instance.Error(" Service Update Failure. Task GUID not found!!");
        //                return;
        //            }

        //            //Poll status or Completed Job Update
        //            if (resp is TaskCompletionResponse)
        //            {
        //                #region TaskCompletionResponse

        //                TaskCompletionResponse rTaskCompletionRepsonse = resp as TaskCompletionResponse;

        //                //Call to RequestTaskStatus() was successful when:
        //                //rTaskStatusResponse is not NULL
        //                //rTaskStatusResponse is marked with success and
        //                //GUID of rTaskStatusResponse and rTaskStatusRequest matches 
        //                if (rTaskCompletionRepsonse.Success)
        //                {
        //                    if (rTaskCompletionRepsonse.Finished == true)
        //                    {

        //                        string msg = "\n Task: " + rTaskCompletionRepsonse.TaskName
        //                               + " Master Guid: " + resp.masterIDRef.ToString()
        //                               + " Guid: " + resp.itemIDRef.ToString() + " is Complete."
        //                               + "\n" + resp.Status.comment;
        //                        JLog.Instance.Error(msg);
        //                        JLog.Instance.AppInfo("任务完成应答，从EAWS请求列表中移除任务");
        //                        GlobalInfo.Instance.JobsRunning.Remove(resp.itemIDRef);

        //                        if (rTaskCompletionRepsonse.OutputLocation.Length > 0)
        //                        {
        //                            JLog.Instance.Error("\nOutput Location: " + rTaskCompletionRepsonse.OutputLocation);
        //                            //IDC_RESULT_TEXT.Text += "\nOutput Location: " + rTaskCompletionRepsonse.OutputLocation;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        string Msg = "\n Task: " + rTaskCompletionRepsonse.TaskName
        //                            + " Master Guid: " + resp.masterIDRef.ToString()
        //                            + " Guid: " + resp.itemIDRef.ToString() + " is Running.";
        //                        JLog.Instance.Error(Msg);
        //                    }
        //                }
        //                else
        //                {
        //                    string Msg = "\n Unable to get status update for Task: "
        //                     + rTaskCompletionRepsonse.TaskName + " Guid: " + rTaskCompletionRepsonse.itemIDRef.ToString();
        //                    JLog.Instance.Error(Msg);
        //                } 
        //                #endregion
        //            }
        //            else if (resp is StartTaskResponse)
        //            {
        //                #region StartTaskResponse
        //                StartTaskResponse rStartTaskResponse = resp as StartTaskResponse;

        //                //Call to RequestStartTask() was successful when:
        //                //rStartTaskResponse is marked with success
        //                if (rStartTaskResponse.Success)
        //                {
        //                    MSG = "\n Task: " + rStartTaskResponse.TaskName
        //                        + " Master Guid: " + resp.masterIDRef.ToString()
        //                        + " Guid: " + rStartTaskResponse.itemIDRef.ToString()
        //                        + " " + rStartTaskResponse.Status.comment;
        //                    JLog.Instance.AppInfo(MSG);
        //                }
        //                else
        //                {
        //                    MSG = "\n Unable to start Task: "
        //                        + " Master Guid: " + resp.masterIDRef.ToString()
        //                        + rStartTaskResponse.TaskName + " Guid: " + rStartTaskResponse.itemIDRef.ToString();
        //                    JLog.Instance.AppInfo(MSG);
        //                } 
        //                #endregion
        //            }
        //            else if (resp is StopTaskResponse)
        //            {
        //                #region StopTaskResponse
        //                StopTaskResponse rStopTaskResponse = resp as StopTaskResponse;

        //                //Call to RequestStopTask() was successful when:
        //                //rStartTaskResponse is marked with success
        //                if (rStopTaskResponse.Success)
        //                {
        //                    MSG = "\n Task: " + rStopTaskResponse.TaskName
        //                         + " Master Guid: " + resp.masterIDRef.ToString()
        //                         + " Guid: " + rStopTaskResponse.itemIDRef.ToString()
        //                         + " " + rStopTaskResponse.Status.comment;
        //                    JLog.Instance.AppInfo(MSG);
        //                }
        //                else
        //                {
        //                    MSG = "\n Unable to stop Task: "
        //                        + " Master Guid: " + resp.masterIDRef.ToString()
        //                        + rStopTaskResponse.TaskName + " Guid: " + rStopTaskResponse.itemIDRef.ToString();
        //                    JLog.Instance.AppInfo(MSG);
        //                } 
        //                #endregion
        //            }
        //            else if (resp is DeleteTaskResponse)
        //            {
        //                #region DeleteTaskResponse
        //                DeleteTaskResponse rDeleteTaskResponse = resp as DeleteTaskResponse;

        //                //Call to RequestDeleteTask() was successful when:
        //                //rDeleteTaskResponse is marked with success
        //                if (rDeleteTaskResponse.Success)
        //                {
        //                    MSG = "\n Task: " + rDeleteTaskResponse.TaskName
        //                        + " Master Guid: " + resp.masterIDRef.ToString()
        //                        + " Guid: " + rDeleteTaskResponse.itemIDRef.ToString()
        //                        + " " + rDeleteTaskResponse.Status.comment;
        //                    JLog.Instance.AppInfo(MSG);
        //                }
        //                else
        //                {
        //                    MSG = "\n Unable to delete Task: "
        //                         + " Master Guid: " + resp.masterIDRef.ToString()
        //                         + rDeleteTaskResponse.TaskName + " Guid: " + rDeleteTaskResponse.itemIDRef.ToString();
        //                    JLog.Instance.AppInfo(MSG);
        //                } 
        //                #endregion
        //            }
        //            else if (resp is TaskStatusResponse)
        //            {
        //                #region TaskStatusResponse
        //                TaskStatusResponse rTaskStatusResponse = resp as TaskStatusResponse;

        //                //Call to RequestTaskStatus() was successful when:
        //                //rTaskStatusResponse is not NULL
        //                //rTaskStatusResponse is marked with success and
        //                //GUID of rTaskStatusResponse and rTaskStatusRequest matches 
        //                if (rTaskStatusResponse.Success)
        //                {
        //                    JLog.Instance.AppInfo("任务状态应答，从EAWS请求列表中移除任务");
        //                    GlobalInfo.Instance.JobsRunning.Remove(resp.itemIDRef);
        //                    MSG = "\n Task: " + rTaskStatusResponse.TaskName
        //                        + " Master Guid: " + resp.masterIDRef.ToString()
        //                        + " Guid: " + rTaskStatusResponse.itemIDRef.ToString();
        //                    JLog.Instance.AppInfo(MSG);

        //                    //Access the Number of warnings, Errors, Outstanding pieces of work and
        //                    //Merge activities of running task.
        //                    //If Outstanding pieces of work is zero then task is finished
        //                    MSG = "\n Succeeded: " + rTaskStatusResponse.NumSucceeded.ToString()
        //                        + " Warnings: " + rTaskStatusResponse.NumWarning.ToString()
        //                        + " Errors: " + rTaskStatusResponse.NumError.ToString()
        //                        + " Outstanding: " + rTaskStatusResponse.NumOutStanding.ToString()
        //                        + " To Merge: " + rTaskStatusResponse.NumAwaitingMerge.ToString()
        //                        + rTaskStatusResponse.Status.comment;
        //                    JLog.Instance.AppInfo(MSG);
        //                }
        //                else
        //                {
        //                    MSG = "\n Unable to get status update for Task: "
        //                        + " Master Guid: " + resp.masterIDRef.ToString()
        //                        + rTaskStatusResponse.TaskName + " Guid: " + rTaskStatusResponse.itemIDRef.ToString();
        //                    JLog.Instance.AppInfo(MSG);
        //                } 
        //                #endregion
        //            }
        //            else if (resp is EditTaskRegionResponse)
        //            {
        //                #region EditTaskRegionResponse
        //                EditTaskRegionResponse rEditTaskRegionResponse = resp as EditTaskRegionResponse;

        //                //Call to RequestEditTaskRegion() was successful when:
        //                //rEditTaskRegionResponse is marked with success 
        //                if (rEditTaskRegionResponse.Success)
        //                {
        //                    MSG = "\n Task: " + rEditTaskRegionResponse.TaskName
        //                        + " Master Guid: " + resp.masterIDRef.ToString()
        //                        + " Guid: " + rEditTaskRegionResponse.itemIDRef.ToString()
        //                        + " " + rEditTaskRegionResponse.Status.comment;
        //                    JLog.Instance.AppInfo(MSG);
        //                }
        //                else
        //                {
        //                    MSG = "\n Unable to edit region for Task: "
        //                        + " Master Guid: " + resp.masterIDRef.ToString()
        //                        + rEditTaskRegionResponse.TaskName + " Guid: " + rEditTaskRegionResponse.itemIDRef.ToString();
        //                    JLog.Instance.AppInfo(MSG);
        //                } 
        //                #endregion
        //            }
        //            else if (resp is EditTaskFiltersResponse)
        //            {
        //                #region EditTaskFiltersResponse
        //                EditTaskFiltersResponse rEditTaskFiltersResponse = resp as EditTaskFiltersResponse;

        //                //Call to RequestEditTaskFilters() was successful when:
        //                //rEditTaskFiltersResponse is marked with success 
        //                if (rEditTaskFiltersResponse.Success)
        //                {
        //                    MSG = "\n Task: " + rEditTaskFiltersResponse.TaskName
        //                         + " Master Guid: " + resp.masterIDRef.ToString()
        //                         + " Guid: " + rEditTaskFiltersResponse.itemIDRef.ToString()
        //                         + " " + rEditTaskFiltersResponse.Status.comment;
        //                    JLog.Instance.AppInfo(MSG);
        //                }
        //                else
        //                {
        //                    MSG = "\n Unable to edit region for Task: "
        //                        + " Master Guid: " + resp.masterIDRef.ToString()
        //                        + rEditTaskFiltersResponse.TaskName + " Guid: " + rEditTaskFiltersResponse.itemIDRef.ToString();
        //                    JLog.Instance.AppInfo(MSG);
        //                } 
        //                #endregion
        //            }
        //            else if (resp is AllSchemaNamesResponse)
        //            {
        //                #region AllSchemaNamesResponse
        //                ////Call RequestAllSchemaNames passing rAllSchemaNamesRequest as a parameter
        //                ////It gets a AllSchemaNamesResponse as a return parameter
        //                //AllSchemaNamesResponse rAllSchemaNamesResponse = resp as AllSchemaNamesResponse;

        //                ////Call to RequestAllSchemaNames() was successful when:
        //                ////rAllSchemaNamesResponse is marked with success and
        //                ////rAllSchemaNamesResponse.AllSchemaNames is not empty as this holds list of all schemas
        //                //if (rAllSchemaNamesResponse.Success && rAllSchemaNamesResponse.AllSchemaNames != null)
        //                //{
        //                //    //Extract each schema from rAllSchemaNamesResponse.AllSchemaNames
        //                //    this.IDC_SCHEMA_COMBO.Items.Clear();
        //                //    foreach (string taskname in rAllSchemaNamesResponse.AllSchemaNames)
        //                //    {
        //                //        this.IDC_SCHEMA_COMBO.Items.Add(taskname);
        //                //    }
        //                //}
        //                //else
        //                //{
        //                //    IDC_RESULT_TEXT.Text += "\n Unable to get list of schemas.";
        //                //}
        //                JLog.Instance.AppInfo("接收到AllSchemaNamesResponse"); 
        //                #endregion
        //            }
        //            else if (resp is AllSchemaTaskNamesResponse)
        //            {
        //                #region AllSchemaTaskNamesResponse
        //                //AllSchemaTaskNamesResponse rAllSchemaTaskNamesResponse = resp as AllSchemaTaskNamesResponse;

        //                ////Call to RequestAllTaskNames() was successful when:
        //                ////rAllSchemaTaskNamesResponse is marked with success and
        //                ////rAllSchemaTaskNamesResponse.AllTaskNames is not empty as this holds list of all tasks
        //                //if (rAllSchemaTaskNamesResponse.Success && rAllSchemaTaskNamesResponse.AllTaskNames != null)
        //                //{
        //                //    //Extract each task from rAllSchemaNamesResponse.AllTaskNames
        //                //    this.IDC_TASK_COMBO.Items.Clear();
        //                //    foreach (string taskname in rAllSchemaTaskNamesResponse.AllTaskNames)
        //                //    {
        //                //        this.IDC_TASK_COMBO.Items.Add(taskname);
        //                //    }
        //                //}
        //                //else
        //                //{
        //                //    IDC_RESULT_TEXT.Text += "\n Unable to get list of tasks for schema: "
        //                //        + rAllSchemaTaskNamesResponse.SchemaName;
        //                //}
        //                JLog.Instance.AppInfo("接收到AllSchemaTaskNamesResponse"); 
        //                #endregion
        //            }
        //        }
        //        //IDC_RESULT_TEXT.ScrollToEnd();
        //    }
        //    catch (Exception ex)
        //    {
        //            JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
        //                MethodBase.GetCurrentMethod().Module.Name);
        //    }
        //}

        public bool StartTaskREQ(string SchemaName,string TaskName)
         {
             try
             {
                //Create a new TaskRequest Object and create a new message GUID
                StartTaskRequest rStartTaskRequest = new StartTaskRequest();
                rStartTaskRequest.itemID = Guid.NewGuid();
                rStartTaskRequest.SchemaName = SchemaName;
                 rStartTaskRequest.TaskName = TaskName;
                JLog.Instance.AppInfo(string.Format( "发送启动任务至EAWS服务,任务名称{0}",TaskName));
                JLog.Instance.AppInfo(string.Format("添加EWAW请求－－启动任务任务序号：{0}", rStartTaskRequest.itemID));
                GlobalInfo.Instance.JobsRunning[rStartTaskRequest.itemID] = rStartTaskRequest;
                //GlobalInfo.Instance.JobsRunning.Add(rStartTaskRequest.itemID, rStartTaskRequest);
                m_EAWSClient.ControlRequest(rStartTaskRequest);
                 return true;
             }
             catch (Exception ex)
             {
                 JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                     MethodBase.GetCurrentMethod().Module.Name);
                 return false;

             }
         }

        #region 自定义事件

        #region 编辑仿真范围应答

        protected event Action<bool, string> EditRegionAckEvent;

        protected void SubDoEditRegionAck(bool Success, string Msg)
        {
            JLog.Instance.AppInfo(string.Format("业务层收到编辑仿真范围应答。。{0}",Success) );
            RaiseEditRegionAckEvent(Success, Msg);
        }

        protected void RaiseEditRegionAckEvent(bool Success, string Msg)
        {
            try
            {
                if (EditRegionAckEvent != null)
                {
                    //EditRegionAckEvent.BeginInvoke(Success, Msg, null, null);
                    EditRegionAckEvent(Success, Msg);
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

        public void RegistEditRegionAckEvent(Action<bool, string> handle)
        {
            DeRegistEditRegionAckEvent(handle);
            EditRegionAckEvent = handle;


        }

        public void DeRegistEditRegionAckEvent(Action<bool, string> handle)
        {
            EditRegionAckEvent = null;

        }

        #endregion

        #region EAWS仿真任务启动状态

        protected event Action<bool, string> EAWSTaskStartStateEvent;

        protected void SubDoEAWSTaskStartState(bool Success, string Msg)
        {
            JLog.Instance.AppInfo("任务启动应答");
            RaiseEAWSTaskStartStateEvent(Success, Msg);
        }

        protected void RaiseEAWSTaskStartStateEvent(bool Success, string Msg)
        {
            if (EAWSTaskStartStateEvent != null)
            {
                //EAWSTaskStartStateEvent.BeginInvoke(Success, Msg, null, null);
                EAWSTaskStartStateEvent(Success, Msg);
            }
        }

        public void RegistEAWSTaskStartStateEvent(Action<bool, string> handle)
        {
            DeRegistEAWSTaskStartStateEvent(handle);
            EAWSTaskStartStateEvent = handle;


        }

        public void DeRegistEAWSTaskStartStateEvent(Action<bool, string> handle)
        {
            EAWSTaskStartStateEvent = null;

        }

        #endregion

        #region 仿真任务执行结果应答

        protected event Action<bool, string> EAWSTaskCompletAckEvent;

        protected void SubDoEAWSTaskCompletAck(bool Success, string SavePath)
        {
            JLog.Instance.AppInfo("仿真执行结果应答");
            RaiseEAWSTaskCompletAckEvent(Success, SavePath);
        }

        protected void RaiseEAWSTaskCompletAckEvent(bool Success, string SavePath)
        {
            //EAWSTaskCompletAckEvent.BeginInvoke(Success, SavePath, null, null);
            EAWSTaskCompletAckEvent(Success, SavePath);
        }

        public void RegistEAWSTaskCompletAckEvent(Action<bool, string> handle)
        {
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
