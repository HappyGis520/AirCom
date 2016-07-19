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
            JLog.Instance.AppInfo("在ＥＡＷＳ回调方法上注册事件方法");
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
