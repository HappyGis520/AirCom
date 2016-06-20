using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using JLIB.Utility;
using NetPlanClient;


namespace NetAnaly
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //处理未捕获的异常   
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常   
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常   
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Control.CheckForIllegalCrossThreadCalls = false;


            if (JLIB.CSharp.Stuff.RunningInstance() == null)
            {
                Application.Run(new FrmMain());
            }
            else
            {
                MessageBox.Show("程序已运行");
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            JLog.Instance.Error(e.Exception.Message);
            
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            JLog.Instance.Error(e.ExceptionObject.ToString());

        }
    }
}
