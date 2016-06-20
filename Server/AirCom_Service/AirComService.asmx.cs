using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using JLIB.CSharp;
using JLIB.Utility;
using NetPlan.BLL;
using NetPlan.Model;

namespace AirCom_Service
{
    /// <summary>
    /// AirComService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://AircomPlan.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class AirComService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool CreateTask(PLAData Data)
        {
            try
            {
                JLog.Instance.AppInfo(string.Format("接收到调用请求,工程名{0}", Data.ProjectName));
                var Dir = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "XML\\";
                string FileName = string.Format("{1}SendXML{0}.xml", DateTime.Now.ToString("hh-mm-ss"), Dir);
                JLog.Instance.AppInfo(FileName);
                JFileExten.ToXML(Data,FileName);
                BLLDoTask.Instance.CreateTask(Data);
                return true;
            }
            catch (Exception ex)
            {
                    JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                        MethodBase.GetCurrentMethod().Module.Name);
                return false;
            }

        }
        [WebMethod]
        public bool CreateTaskByXML(string Data)
        {
            try
            {
                JLog.Instance.AppInfo("通过ＸＭＬ串传递");
                var Dir = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "XML\\";
                string FileName = string.Format("{1}SendXML{0}.xml", DateTime.Now.ToString("hh-mm-ss"), Dir);


                string Msg = string.Empty;
                JFileExten.SaveToCreateFile(FileName, Data, out Msg);
                var obj =  XMLHelper.XmlDeserialize<PLAData>(Data);
                JLog.Instance.AppInfo(string.Format("接收到调用请求,工程名{0}", obj.ProjectName));
                //JLog.Instance.AppInfo(string.Format("接收到浪潮调用请求,工程名{0}", Data.ProjectName));
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        [WebMethod]
        public bool HelloWord()
        {
            JLog.Instance.AppInfo("收到请求.....");
            return true;
        }
        
    }
}
