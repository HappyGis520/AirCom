using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JFrameWork.Model;
using JFrameWork.WCFClient;
using JLIB.CSharp;
using JLIB.Utility;
using NetPlanClient.BLL.AirComSvr;


namespace NetPlanClient.BLL
{
    public class BLLMain:Singleton<BLLMain>
    {
        AirComSvr.AirComService AircomSvr =null;
        public BLLMain()
        {
            AircomSvr = new AirComService();
            RegistEventHandle();
            
        }

        private void RegistEventHandle()
        {
            
        }
        /// <summary>
        /// 执行规划仿真任务
        /// </summary>
        public void DoPlanTask(PLAData Data)
        {
            JLog.Instance.AppInfo("开始调用服务");
            AircomSvr.CreateTask(Data);

            JLog.Instance.AppInfo("开始调用服务完成");
        }

    }
}
