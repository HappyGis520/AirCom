using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using JLIB.CSharp;
using JLIB.Utility;

using Service.Model;
using Service.Contract;
using Service.Services;

namespace ARSService
{
    public partial class Service1 : ServiceBase
    {
        private ServiceHost m_serviceHost = null;
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            try
            {
                Console.WriteLine("正在初始化服务.....");
                //using (m_serviceHost = new ServiceHost(typeof(ARSServer)))
                //{
                    m_serviceHost = new ServiceHost(typeof (ARSServer));
                    m_serviceHost.Opened += delegate
                    {
                        JLog.Instance.AppDebug("ARSService已经启动！");
                    };
                    m_serviceHost.Opening += delegate
                    {
                        JLog.Instance.AppDebug("ARSService启动中，请稍候....");
                    };
                    m_serviceHost.Closed += delegate
                    {
                        JLog.Instance.AppDebug("ARSServices已停止");
                    };
                    m_serviceHost.Open();
                    //bool Quite = false;
                    //do
                    //{
                    //    Thread.Sleep(100000);

                    //} while (!Quite);
                //}

            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }

        }

        protected override void OnStop()
        {
            JLog.Instance.AppDebug("ARSServices已停止");
        }

    }
    
}
