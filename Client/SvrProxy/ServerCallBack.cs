using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using JFrameWork.Model;
using JFrameWork.WCFServer;
using JFrameWork.WCFClient;
using JLIB.Utility;
using NetPlan.IContract;
using NetPlan.Model;


namespace NetPlannerClient.ServerProxy
{
    internal partial class ServerCallBack : WCFServerCallBackBase,INetPlanCallBackContract
    {
        //public override void UserLoginACK(bool Succes, LoginUser User, string Msg)
        //{
        //    base.UserLoginACK(Succes, User, Msg);
        //}
    }
}
