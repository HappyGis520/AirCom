using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EAWSTestApp.EAWSService;
using System.ServiceModel;
using System.Collections;
using System.Reflection;
using JLIB.Utility;


namespace EAWSTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //This class implements EAWSService.EAWSCallback interface and gets passed to the EAWS Service
    //and is used to receive any "duplex" call back messages from the EAWS Service
    //The class has a MainWindow member variable which gets set inside the constructor
    //to hold a reference to the Test Client.  
    public class EAWSCallBackSolid : EAWSService.EAWSCallback
    {
        public EAWSCallBackSolid(MainWindow rWindow)
        {
            m_MainWindow = rWindow;
        }

        //The function is called by the EAWS Service. This in turn calls the Test Client SendUpdate() function
        public void SendUpdate(EAWSTestApp.EAWSService.JobResponseType resp)
        {
           
            m_MainWindow.SendUpdate(resp);
        }

        MainWindow m_MainWindow;
    }

    public partial class MainWindow : Window
    {
            
        public enum TaskActionStatus
        {
            ACTION_OK,
            ACTION_CANCEL
        };

        string sEndpointName = "EAWSHTTPService";


        public MainWindow()
        {
            InitializeComponent();

            //This code constructs a EAWSCallBackSolid object
            //and creates a EAWS service object and passes EAWSCallBackSolid object
            // m_EAWSClient = (EAWSService.IEAWSService)DuplexChannelManager<IEAWSService>.GetAutoChannelWithServerOverride("EAWSService", "", _callbackContext);
            InstanceContext _callbackContext = new InstanceContext(new EAWSCallBackSolid(this));
            m_EAWSClient = new EAWSClient(_callbackContext, sEndpointName);
            m_JobsRunning = new Hashtable();
            this.IDC_ENDPOINT_COMBO.Items.Add("EAWSTCPService");
            this.IDC_ENDPOINT_COMBO.Items.Add("EAWSHTTPService");
            this.IDC_ENDPOINT_COMBO.SelectedValue = "EAWSHTTPService";



        }

      /// <summary>
      /// 测试终结点连接
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void IDC_BUTTON_TEST_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                
                string SelectedEndpointName = this.IDC_ENDPOINT_COMBO.SelectedItem.ToString();

                InstanceContext _callbackContext = new InstanceContext(new EAWSCallBackSolid(this));
                m_EAWSClient = new EAWSClient(_callbackContext, SelectedEndpointName);

                string ServiceString = m_EAWSClient.SyncIsAWSchedulerStarted();//调度模块是否已启动

                if (ServiceString.Length > 0) //调度会话是否正在运行，返回的字符大于0表示停止了  scheduler stopped == msg, 0 len == running
                {
                    IDC_RESULT_TEXT.Text += "开始启动调度模块……" + "\n";
                    ServiceString = m_EAWSClient.SyncStartAWScheduler();
                    IDC_RESULT_TEXT.Text += ServiceString + "\n";

                }
                else
                {
                    IDC_RESULT_TEXT.Text += "调度模块正在运行中……" + "\n";
                    
                }
            }
            catch (Exception ex)
            {
              JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                  MethodBase.GetCurrentMethod().Module.Name);
            }
        }

        //This event handler function starts a task by passing the schema name and task name to the EAWS Service
        //and calling RequestStartTask()
        private void IDC_StartTaskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDC_SCHEMA_COMBO.SelectedItem != null && IDC_TASK_COMBO.SelectedItem != null && 
                    IDC_SCHEMA_COMBO.SelectedItem.ToString() != "" && IDC_TASK_COMBO.SelectedItem.ToString() != "")
                {
                    //Create a new TaskRequest Object and create a new message GUID
                    EAWSService.StartTaskRequest rStartTaskRequest = new StartTaskRequest();
                    rStartTaskRequest.itemID = Guid.NewGuid();

                    //Set schma name and task name
                    rStartTaskRequest.SchemaName = IDC_SCHEMA_COMBO.SelectedItem.ToString();
                    rStartTaskRequest.TaskName = IDC_TASK_COMBO.SelectedItem.ToString();

                    //Store Job GUID for Callback
                    m_JobsRunning[rStartTaskRequest.itemID] = rStartTaskRequest;

                    //  Call RequestStartTask passing rStartTaskRequest as a parameter
                    m_EAWSClient.ControlRequest(rStartTaskRequest);
                    
                }
            }
            catch (Exception eException)
            {
                MessageBox.Show(eException.Message);
            }
        }

        //This event handler function stops a task by passing the schema name and task name to the EAWS Service
        //and calling RequestStopTask()
        private void IDC_STOP_TASK_BUT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDC_SCHEMA_COMBO.SelectedItem != null && IDC_TASK_COMBO.SelectedItem != null && 
                    IDC_SCHEMA_COMBO.SelectedItem.ToString() != "" && IDC_TASK_COMBO.SelectedItem.ToString() != "")
                {
                    //Create a new TaskRequest Object and create a new message GUID
                    EAWSService.StopTaskRequest rStopTaskRequest = new StopTaskRequest();
                    rStopTaskRequest.itemID = Guid.NewGuid();

                    //Set schma name and task name
                    rStopTaskRequest.SchemaName = IDC_SCHEMA_COMBO.SelectedItem.ToString();
                    rStopTaskRequest.TaskName = IDC_TASK_COMBO.SelectedItem.ToString();

                    //Store Job GUID for Callback
                    m_JobsRunning[rStopTaskRequest.itemID] = rStopTaskRequest;

                    //  Call RequestStopTask passing rStopTaskRequest as a parameter
                    m_EAWSClient.ControlRequest(rStopTaskRequest);
                }
            }
            catch (Exception eException)
            {
                MessageBox.Show(eException.Message);
            }
        }

        //This event handler function retrieves all schemas from the Array Wizard database
        //by calling RequestAllSchemaNames()
        private void IDC_LOAD_SCHEMAS_BUT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Create a new AllSchemaNamesRequest Object and create a new message GUID
                EAWSService.AllSchemaNamesRequest rAllSchemaNamesRequest = new AllSchemaNamesRequest();
                rAllSchemaNamesRequest.itemID = Guid.NewGuid();

                //Store Job GUID for Callback
                m_JobsRunning[rAllSchemaNamesRequest.itemID] = rAllSchemaNamesRequest;

                //Call RequestAllSchemaNames passing rAllSchemaNamesRequest as a parameter
                m_EAWSClient.QueryRequest(rAllSchemaNamesRequest);
                
            }
            catch (Exception eException)
            {
                MessageBox.Show("Exception is: " + eException.Message);
            }
        }

        //This event handler function retrieves all tasks that belongs to selected Schena
        //by calling RequestAllSchemaNames()
        private void IDC_LOAD_TASKS_BUT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDC_SCHEMA_COMBO.SelectedItem != null && IDC_SCHEMA_COMBO.SelectedItem.ToString() != "")
                {
                    //Create a new AllSchemaTaskNamesRequest Object and create a new message GUID
                    EAWSService.AllSchemaTaskNamesRequest rAllSchemaTaskNamesRequest = new AllSchemaTaskNamesRequest();
                    rAllSchemaTaskNamesRequest.itemID = Guid.NewGuid();

                    //Set task name
                    rAllSchemaTaskNamesRequest.SchemaName = IDC_SCHEMA_COMBO.SelectedItem.ToString();

                    //Store Job GUID for Callback
                    m_JobsRunning[rAllSchemaTaskNamesRequest.itemID] = rAllSchemaTaskNamesRequest;

                    //Call RequestAllTaskNames passing rAllSchemaTaskNamesRequest as a parameter
                    m_EAWSClient.QueryRequest(rAllSchemaTaskNamesRequest);
                }
                else
                {
                    IDC_RESULT_TEXT.Text += "\n Please Select a Schema";
                }
            }
            catch (Exception eException)
            {
                MessageBox.Show("Exception is: " + eException.Message);
            }
        }


        //This event handler function polls for the status of a task by passing the schema name and task name
        // to the EAWS Service and calling RequestTaskStatus()
        private void IDC_TASK_STATUS_BUT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDC_SCHEMA_COMBO.SelectedItem != null && IDC_TASK_COMBO.SelectedItem != null && 
                    IDC_SCHEMA_COMBO.SelectedItem.ToString() != "" && IDC_TASK_COMBO.SelectedItem.ToString() != "")
                {
                    //Create a new TaskRequest Object and create a new message GUID
                    EAWSService.TaskStatusRequest rTaskStatusRequest = new TaskStatusRequest();
                    rTaskStatusRequest.itemID = Guid.NewGuid();

                    //Set schma name and task name
                    counter++;
                    rTaskStatusRequest.SchemaName = IDC_SCHEMA_COMBO.SelectedItem.ToString();
                    rTaskStatusRequest.TaskName = IDC_TASK_COMBO.SelectedItem.ToString();// +" " + counter.ToString();

                    //Store Job GUID for Callback
                    m_JobsRunning[rTaskStatusRequest.itemID] = rTaskStatusRequest;

                    //Call RequestTaskStatus passing rTaskStatusRequest as a parameter
                    //It gets a TaskStatusResponse as a return parameter
                    m_EAWSClient.QueryRequest(rTaskStatusRequest);
                }
            }
            catch (Exception eException)
            {
                MessageBox.Show(eException.Message);
            }
        }


        //This event handler function deletes a task by passing the schema name and task name
        // to the EAWS Service and calling RequestDeleteTask()
        private void IDC_DELETE_TASK_BUT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDC_SCHEMA_COMBO.SelectedItem != null && IDC_TASK_COMBO.SelectedItem != null && 
                    IDC_SCHEMA_COMBO.SelectedItem.ToString() != "" && IDC_TASK_COMBO.SelectedItem.ToString() != "")
                {
                    //Create a new TaskRequest Object and create a new message GUID
                    EAWSService.DeleteTaskRequest rDeleteTaskRequest = new DeleteTaskRequest();
                    rDeleteTaskRequest.itemID = Guid.NewGuid();

                    //Set schma name and task name
                    rDeleteTaskRequest.SchemaName = IDC_SCHEMA_COMBO.SelectedItem.ToString();
                    rDeleteTaskRequest.TaskName = IDC_TASK_COMBO.SelectedItem.ToString();

                    //Store Job GUID for Callback
                    m_JobsRunning[rDeleteTaskRequest.itemID] = rDeleteTaskRequest;

                    //Call RequestTaskStatus passing rDeleteTaskRequest as a parameter
                    m_EAWSClient.ControlRequest(rDeleteTaskRequest);
                }
            }
            catch (Exception eException)
            {
                MessageBox.Show(eException.Message);
            }
        }

        //This event handler function lauches the Edit Region dialog
        //If the user selects to update the region it will update the region of a task
        //by passing the schema name, task name and the regions supplied via the Edit Region dialog
        // to the EAWS Service and calling RequestEditTaskRegion()
        private void IDC_EditRegionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditRegion rEditRegion = new EditRegion();
                rEditRegion.ShowDialog();

                if (rEditRegion.UpdateRegion == true)
                {
                    //Create a new EditTaskRegionRequest Object and create a new message GUID
                    EAWSService.EditTaskRegionRequest rEditTaskRegionRequest = new EditTaskRegionRequest();
                    rEditTaskRegionRequest.itemID = Guid.NewGuid();

                    //set the schema name, task name and the region.
                    rEditTaskRegionRequest.SchemaName = IDC_SCHEMA_COMBO.SelectedItem.ToString();
                    rEditTaskRegionRequest.TaskName = IDC_TASK_COMBO.SelectedItem.ToString();
                    rEditTaskRegionRequest.EastMin = Convert.ToInt32(rEditRegion.IDC_EASTMIN.Text);
                    rEditTaskRegionRequest.EastMax = Convert.ToInt32(rEditRegion.IDC_EASTMAX.Text);
                    rEditTaskRegionRequest.NorthMin = Convert.ToInt32(rEditRegion.IDC_NORTHMIN.Text);
                    rEditTaskRegionRequest.NorthMax = Convert.ToInt32(rEditRegion.IDC_NORTHMAX.Text);

                    //Store Job GUID for Callback
                    m_JobsRunning[rEditTaskRegionRequest.itemID] = rEditTaskRegionRequest;

                    //Call RequestEditTaskRegion passing rEditTaskRegionRequest as a parameter
                    m_EAWSClient.EditRequest(rEditTaskRegionRequest);
                }
            }
            catch (Exception eException)
            {
                MessageBox.Show(eException.Message);
            }
        }


        //This event handler function lauches the Edit Filter dialog
        //If the user selects to update the Filter it will update the Filter of a task
        //by passing the schema name, task name and the Filter supplied via the Edit Filter dialog
        // to the EAWS Service and calling RequestEditTaskFilters()
        private void IDC_EditFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditFilters rEditFilters = new EditFilters();
                rEditFilters.ShowDialog();

                if (rEditFilters.UpdateFilters == true)
                {
                    //Create a new EditTaskRegionRequest Object and create a new message GUID
                    EAWSService.EditTaskFiltersRequest rEditTaskFiltersRequest = new EditTaskFiltersRequest();
                    rEditTaskFiltersRequest.itemID = Guid.NewGuid();

                    //set the schema name, task name and the region.
                    rEditTaskFiltersRequest.SchemaName = IDC_SCHEMA_COMBO.SelectedItem.ToString();
                    rEditTaskFiltersRequest.TaskName = IDC_TASK_COMBO.SelectedItem.ToString();

                    //Set the Plan and Best Server Filters where applicable
                    rEditTaskFiltersRequest.PlanFilters = new string[rEditFilters.IDC_SELECTED_PLANFILTERS.Items.Count];
                    rEditFilters.IDC_SELECTED_PLANFILTERS.Items.CopyTo(rEditTaskFiltersRequest.PlanFilters, 0);

                    rEditTaskFiltersRequest.BestServerFilters = new string[rEditFilters.IDC_SELECTED_BESTSERVERFILTERS.Items.Count];
                    rEditFilters.IDC_SELECTED_BESTSERVERFILTERS.Items.CopyTo(rEditTaskFiltersRequest.BestServerFilters, 0);

                    //Store Job GUID for Callback
                    m_JobsRunning[rEditTaskFiltersRequest.itemID] = rEditTaskFiltersRequest;

                    //Call RequestEditTaskFilters passing rEditTaskFiltersRequest as a parameter
                    m_EAWSClient.EditRequest(rEditTaskFiltersRequest);
                }
            }
            catch (Exception eException)
            {
                MessageBox.Show(eException.Message);
            }
        }

        //This function gets called by EAWSCallBackSolid object when it receives a call back from EAWS Service.
        //Any call to the EAWSService will get a response via this call back plus on running poll status
        //and completed job status of any running job will be sent via this call back
        //It receives a EAWSService.JobResponseType which can be tested and cast to determine
        //the required repspnse.
        public void SendUpdate(EAWSService.JobResponseType resp)
        {
            try
            {
                lock (lockThis)
                {
                    if(resp == null)
                    {
                        IDC_RESULT_TEXT.Text += "\n Service Update Failure!!";
                        return;
                    }

                    if(m_JobsRunning.ContainsKey(resp.itemIDRef) == false )
                    {
                        IDC_RESULT_TEXT.Text += "\n Service Update Failure. Task GUID not found!!";
                        IDC_RESULT_TEXT.Text += resp.Status.comment; //Might be exception thrown.
                        return;
                    }

                    //Poll status or Completed Job Update
                    if (resp is EAWSService.TaskCompletionResponse)
                    {

                        EAWSService.TaskCompletionResponse rTaskCompletionRepsonse = resp as EAWSService.TaskCompletionResponse;

                        //Call to RequestTaskStatus() was successful when:
                        //rTaskStatusResponse is not NULL
                        //rTaskStatusResponse is marked with success and
                        //GUID of rTaskStatusResponse and rTaskStatusRequest matches 
                        if (rTaskCompletionRepsonse.Success)
                        {
                            if (rTaskCompletionRepsonse.Finished == true)
                            {
                                IDC_RESULT_TEXT.Text += "\n Task: " + rTaskCompletionRepsonse.TaskName
                                       + " Master Guid: " + resp.masterIDRef.ToString()
                                       + " Guid: " + resp.itemIDRef.ToString() + " is Complete."
                                       + "\n" + resp.Status.comment;
                                m_JobsRunning.Remove(resp.itemIDRef);

                                if (rTaskCompletionRepsonse.OutputLocation.Length > 0)
                                {
                                    IDC_RESULT_TEXT.Text += "\nOutput Location: " + rTaskCompletionRepsonse.OutputLocation;
                                }
                            }
                            else
                            {
                                IDC_RESULT_TEXT.Text += "\n Task: " + rTaskCompletionRepsonse.TaskName
                                    + " Master Guid: " + resp.masterIDRef.ToString()
                                    + " Guid: " + resp.itemIDRef.ToString() + " is Running.";
                            }
                        }
                        else
                        {
                            IDC_RESULT_TEXT.Text += "\n Unable to get status update for Task: "
                            + rTaskCompletionRepsonse.TaskName + " Guid: " + rTaskCompletionRepsonse.itemIDRef.ToString();
                        }
                    }
                    else if (resp is EAWSService.StartTaskResponse)
                    {
                        EAWSService.StartTaskResponse rStartTaskResponse = resp as EAWSService.StartTaskResponse;

                         //Call to RequestStartTask() was successful when:
                         //rStartTaskResponse is marked with success
                         if (rStartTaskResponse.Success)
                         {
                             IDC_RESULT_TEXT.Text += "\n Task: " + rStartTaskResponse.TaskName
                                 + " Master Guid: " + resp.masterIDRef.ToString()
                                 + " Guid: " + rStartTaskResponse.itemIDRef.ToString()
                                 + " " + rStartTaskResponse.Status.comment;
                         }
                         else
                         {
                             IDC_RESULT_TEXT.Text += "\n Unable to start Task: "
                                 + " Master Guid: " + resp.masterIDRef.ToString()
                                 + rStartTaskResponse.TaskName + " Guid: " + rStartTaskResponse.itemIDRef.ToString();
                         }
                    }
                    else if (resp is EAWSService.StopTaskResponse)
                    {
                        EAWSService.StopTaskResponse rStopTaskResponse = resp as EAWSService.StopTaskResponse;

                        //Call to RequestStopTask() was successful when:
                        //rStartTaskResponse is marked with success
                        if (rStopTaskResponse.Success)
                        {
                            IDC_RESULT_TEXT.Text += "\n Task: " + rStopTaskResponse.TaskName
                                + " Master Guid: " + resp.masterIDRef.ToString()
                                + " Guid: " + rStopTaskResponse.itemIDRef.ToString()
                                + " " + rStopTaskResponse.Status.comment;
                        }
                        else
                        {
                            IDC_RESULT_TEXT.Text += "\n Unable to stop Task: "
                                + " Master Guid: " + resp.masterIDRef.ToString()
                                + rStopTaskResponse.TaskName + " Guid: " + rStopTaskResponse.itemIDRef.ToString();
                        }
                    }
                    else if (resp is EAWSService.DeleteTaskResponse)
                    {
                        EAWSService.DeleteTaskResponse rDeleteTaskResponse = resp as EAWSService.DeleteTaskResponse;

                        //Call to RequestDeleteTask() was successful when:
                        //rDeleteTaskResponse is marked with success
                        if (rDeleteTaskResponse.Success)
                        {
                            IDC_RESULT_TEXT.Text += "\n Task: " + rDeleteTaskResponse.TaskName
                                + " Master Guid: " + resp.masterIDRef.ToString()
                                + " Guid: " + rDeleteTaskResponse.itemIDRef.ToString()
                                + " " + rDeleteTaskResponse.Status.comment;
                        }
                        else
                        {
                            IDC_RESULT_TEXT.Text += "\n Unable to delete Task: "
                                + " Master Guid: " + resp.masterIDRef.ToString()
                                + rDeleteTaskResponse.TaskName + " Guid: " + rDeleteTaskResponse.itemIDRef.ToString();
                        }
                    }
                    else if (resp is EAWSService.TaskStatusResponse)
                    {
                        EAWSService.TaskStatusResponse rTaskStatusResponse = resp as EAWSService.TaskStatusResponse;

                        //Call to RequestTaskStatus() was successful when:
                        //rTaskStatusResponse is not NULL
                        //rTaskStatusResponse is marked with success and
                        //GUID of rTaskStatusResponse and rTaskStatusRequest matches 
                        if (rTaskStatusResponse.Success)
                        {
                            m_JobsRunning.Remove(resp.itemIDRef);
                            IDC_RESULT_TEXT.Text += "\n Task: " + rTaskStatusResponse.TaskName
                                + " Master Guid: " + resp.masterIDRef.ToString()
                                + " Guid: " + rTaskStatusResponse.itemIDRef.ToString();

                            //Access the Number of warnings, Errors, Outstanding pieces of work and
                            //Merge activities of running task.
                            //If Outstanding pieces of work is zero then task is finished
                            IDC_RESULT_TEXT.Text += "\n Succeeded: " + rTaskStatusResponse.NumSucceeded.ToString()
                                + " Warnings: " + rTaskStatusResponse.NumWarning.ToString()
                                + " Errors: " + rTaskStatusResponse.NumError.ToString()
                                + " Outstanding: " + rTaskStatusResponse.NumOutStanding.ToString()
                                + " To Merge: " + rTaskStatusResponse.NumAwaitingMerge.ToString()
                                + rTaskStatusResponse.Status.comment;
                        }
                        else
                        {
                            IDC_RESULT_TEXT.Text += "\n Unable to get status update for Task: "
                                + " Master Guid: " + resp.masterIDRef.ToString()
                                + rTaskStatusResponse.TaskName + " Guid: " + rTaskStatusResponse.itemIDRef.ToString();
                        }
                    }
                    else if (resp is EAWSService.EditTaskRegionResponse)
                    {
                        EAWSService.EditTaskRegionResponse rEditTaskRegionResponse = resp as EAWSService.EditTaskRegionResponse;

                        //Call to RequestEditTaskRegion() was successful when:
                        //rEditTaskRegionResponse is marked with success 
                        if (rEditTaskRegionResponse.Success)
                        {
                            IDC_RESULT_TEXT.Text += "\n Task: " + rEditTaskRegionResponse.TaskName
                                + " Master Guid: " + resp.masterIDRef.ToString()
                                + " Guid: " + rEditTaskRegionResponse.itemIDRef.ToString()
                                + " " + rEditTaskRegionResponse.Status.comment;
                        }
                        else
                        {
                            IDC_RESULT_TEXT.Text += "\n Unable to edit region for Task: "
                                + " Master Guid: " + resp.masterIDRef.ToString()
                                + rEditTaskRegionResponse.TaskName + " Guid: " + rEditTaskRegionResponse.itemIDRef.ToString();
                        }
                    }
                    else if (resp is EAWSService.EditTaskFiltersResponse)
                    {
                        EAWSService.EditTaskFiltersResponse rEditTaskFiltersResponse = resp as EAWSService.EditTaskFiltersResponse;

                        //Call to RequestEditTaskFilters() was successful when:
                        //rEditTaskFiltersResponse is marked with success 
                        if (rEditTaskFiltersResponse.Success)
                        {
                            IDC_RESULT_TEXT.Text += "\n Task: " + rEditTaskFiltersResponse.TaskName
                                + " Master Guid: " + resp.masterIDRef.ToString()
                                + " Guid: " + rEditTaskFiltersResponse.itemIDRef.ToString()
                                + " " + rEditTaskFiltersResponse.Status.comment;
                        }
                        else
                        {
                            IDC_RESULT_TEXT.Text += "\n Unable to edit region for Task: "
                                + " Master Guid: " + resp.masterIDRef.ToString()
                                + rEditTaskFiltersResponse.TaskName + " Guid: " + rEditTaskFiltersResponse.itemIDRef.ToString();
                        }
                    }
                    else if (resp is EAWSService.AllSchemaNamesResponse)
                    {
                        //Call RequestAllSchemaNames passing rAllSchemaNamesRequest as a parameter
                        //It gets a AllSchemaNamesResponse as a return parameter
                        EAWSService.AllSchemaNamesResponse rAllSchemaNamesResponse = resp as EAWSService.AllSchemaNamesResponse;

                        //Call to RequestAllSchemaNames() was successful when:
                        //rAllSchemaNamesResponse is marked with success and
                        //rAllSchemaNamesResponse.AllSchemaNames is not empty as this holds list of all schemas
                        if (rAllSchemaNamesResponse.Success && rAllSchemaNamesResponse.AllSchemaNames != null)
                        {
                            //Extract each schema from rAllSchemaNamesResponse.AllSchemaNames
                            this.IDC_SCHEMA_COMBO.Items.Clear();
                            foreach (string taskname in rAllSchemaNamesResponse.AllSchemaNames)
                            {
                                this.IDC_SCHEMA_COMBO.Items.Add(taskname);
                            }
                        }
                        else
                        {
                            IDC_RESULT_TEXT.Text += "\n Unable to get list of schemas.";
                        }
                    }
                    else if (resp is EAWSService.AllSchemaTaskNamesResponse)
                    {
                        EAWSService.AllSchemaTaskNamesResponse rAllSchemaTaskNamesResponse = resp as EAWSService.AllSchemaTaskNamesResponse;

                        //Call to RequestAllTaskNames() was successful when:
                        //rAllSchemaTaskNamesResponse is marked with success and
                        //rAllSchemaTaskNamesResponse.AllTaskNames is not empty as this holds list of all tasks
                        if (rAllSchemaTaskNamesResponse.Success && rAllSchemaTaskNamesResponse.AllTaskNames != null)
                        {
                            //Extract each task from rAllSchemaNamesResponse.AllTaskNames
                            this.IDC_TASK_COMBO.Items.Clear();
                            foreach (string taskname in rAllSchemaTaskNamesResponse.AllTaskNames)
                            {
                                this.IDC_TASK_COMBO.Items.Add(taskname);
                            }
                        }
                        else
                        {
                            IDC_RESULT_TEXT.Text += "\n Unable to get list of tasks for schema: "
                                + rAllSchemaTaskNamesResponse.SchemaName;
                        }
                    }
                }
                IDC_RESULT_TEXT.ScrollToEnd();
            }
            catch (Exception eException)
            {
                MessageBox.Show(eException.Message);
            }
        }

        //Holds a Map of GUIDs and running jobs
        private Hashtable m_JobsRunning;

        //References EAWS Services.
        private EAWSService.EAWSClient m_EAWSClient;
        private System.Object lockThis = new System.Object();
        private int counter = 0;
    }

}
