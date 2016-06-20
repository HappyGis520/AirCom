using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using JLIB.CSharp;
using JLIB.Utility;
using NetPlan.IContract;
using NetPlan.Servcie;


namespace ConsolHoster
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {

                ////处理未捕获的异常   
                //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                ////处理UI线程异常   
                //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                ////处理非UI线程异常   
                //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                StartService();
            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }
        }

        private static void StartService()
        {
            #region 启动服务

            Console.WriteLine("正在初始化服务.....");
            using (ServiceHost host = new ServiceHost(typeof(AircomServcie)))
            {
                host.Opened += delegate
                {

                    JLog.Instance.AppDebug(string.Format("Aircomserver已经启动！" ));
                    //Console.WriteLine("ARSSserver已经启动，按任意键终止服务！");
                    Console.ReadLine();
                };
                host.Opening += delegate
                {
                    JLog.Instance.AppDebug("AircomSserver启动中，请稍候....");
                    //Console.WriteLine("ARSSserver启动中，请稍候....");
                };

                host.Open();
                bool Quite = false;
                do
                {
                    string str = Console.ReadLine();
                    if (str.Trim().ToLower().Equals("exit"))
                    {
                        Quite = true;
                    }
                    if (str.Trim().ToLower().Equals("clear"))
                    {
                        Console.Clear();
                    }
                } while (!Quite);
            }

            #endregion
        }
    }
}
