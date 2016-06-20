using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using JFrameWork.Model;
using JFrameWork.WCFClient;
using JLIB.Utility;
using Microsoft.Win32;
using NetPlan.IContract;
using NetPlan.Model;

namespace NetPlannerClient.ServerProxy
{
    internal partial class ServerProxy : WCFClientBase<ServerProxy>, INetPlanContract
    {

        private ServerCallBack _CallBack = null;
        private InstanceContext _InstanceContext = null;
        private DuplexChannelFactory<INetPlanContract> _channelFactory = null;
        private INetPlanContract _Proxy;

        private LoginUser CurLoginUser
        {
            get { return _CurLoginUser; }
            set { _CurLoginUser = value; }
        }


        public ServerProxy()
        {

            //RegistCallBackEventHandle();
        }


        private void InitonClientProxy()
        {
            if (LinkServerSuccess)
            {
                StopServerLinkNotify();
            }
            _CallBack = new ServerCallBack();
            RegistCallBackEventHandle();
            _InstanceContext = new InstanceContext(_CallBack);
            _channelFactory = new DuplexChannelFactory<INetPlanContract>(_InstanceContext, "AircomEndPoint");
            _Proxy = _channelFactory.CreateChannel();
        }

        private void RegistCallBackEventHandle()
        {
            _CallBack.RegistSayHelloEvent(SubDoSayHelloToClient);
            _CallBack.RegistStartLinkServerACKEvent(LinkServerAckEvent);
            _CallBack.RegistUserLoginACKEvent(SubDoUserLoginAckEvent);
            _CallBack.RegistUserLoginACKEvent(SubDoUserLoginAck);
        }

        protected void DeRegistCallBackEventHandle()
        {
            _CallBack.DeRegistStartLinkServerACKEvent(LinkServerAckEvent);

            _CallBack.DeRegistSayHelloEvent(SubDoSayHelloToClient);
        }

        #region WCFClientBase

        public override void SayHelloToServer()
        {

            try
            {
                _Proxy.SayHelloToServer();
            }
            catch (ChannelTerminatedException)
            {
                JLog.Instance.Warn("信道已销毁。。。");
                StartRelinkserver();

                //this.Dispose();
            }
            catch (CommunicationException)
            {
                JLog.Instance.Warn("信道无法打开并且已进入 Faulted 状态。");
                StartRelinkserver();

                //this.Dispose();
            }
            catch (TimeoutException)
            {
                JLog.Instance.Warn("信道无法打开,打开超时。");
                StartRelinkserver();

                //this.Dispose();
            }
            catch (Exception ex)
            {

                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                StartRelinkserver();
            }
        }


        protected override void DoRelinkServer(object info)
        {
            //StopServerLinkNotify();
            //ReleaseAllProxy();
            StartLinkServerREQ();
        }

        protected override void UserReLoginREQ(LoginUser User)
        {
            if (User != null)
            {
                try
                {
                    MLoginUser sb = User as MLoginUser;
                    LoginRequest(sb.UserObj.UserName, sb.UserObj.Password);
                }
                catch (Exception ex)
                {
                    JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                        MethodBase.GetCurrentMethod().Module.Name);
                }


            }

        }

        public override void LogoutNotify()
        {
            try
            {
                _Proxy.LogoutNotify();
            }
            catch (ChannelTerminatedException)
            {
                JLog.Instance.Warn("信道已销毁。。。");


                //this.Dispose();
            }
            catch (CommunicationException)
            {
                JLog.Instance.Warn("信道无法打开并且已进入 Faulted 状态。");


                //this.Dispose();
            }
            catch (TimeoutException)
            {
                JLog.Instance.Warn("信道无法打开,打开超时。");


                //this.Dispose();
            }
            catch (Exception ex)
            {

                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }


        }


        public override void StartLinkServerREQ()
        {

            try
            {
                InitonClientProxy();
                _Proxy.StartLinkServerREQ();
            }
            catch (ChannelTerminatedException)
            {
                JLog.Instance.Warn("信道已销毁。。。");
                StartRelinkserver();

                //this.Dispose();
            }
            catch (CommunicationException)
            {
                JLog.Instance.Warn("信道无法打开并且已进入 Faulted 状态。");
                StartRelinkserver();

                //this.Dispose();
            }
            catch (TimeoutException)
            {
                JLog.Instance.Warn("信道无法打开,打开超时。");
                StartRelinkserver();

                //this.Dispose();
            }
            catch (Exception ex)
            {

                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
                StartRelinkserver();
            }
        }

        public override void StopServerLinkNotify()
        {
            if (LinkServerSuccess && _Proxy != null)
            {
                try
                {
                    //_Proxy.LogoutNotify();
                    _Proxy.StopServerLinkNotify();
                }
                catch (ChannelTerminatedException)
                {
                    JLog.Instance.Warn("信道已销毁。。。");
                    //this.Dispose();
                }
                catch (CommunicationException)
                {
                    JLog.Instance.Warn("信道无法打开并且已进入 Faulted 状态。");

                    //this.Dispose();
                }
                catch (TimeoutException)
                {
                    JLog.Instance.Warn("信道无法打开,打开超时。");

                    //this.Dispose();
                }
                catch (Exception ex)
                {
                    JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                        MethodBase.GetCurrentMethod().Module.Name);
                }
                finally
                {
                    CurLoginUser = null;
                    _LinkServerSuccess = false;
                    //ReleaseAllProxy();
                }


            }
        } 
        #endregion

        public void StartServiceREQ()
        {

        }

        public void StopServiceREQ()
        {

        }

        public void LoginRequest(string userid, string userpwd)
        {
            _Proxy.LoginRequest(userid, userpwd);

        }

        #region 事件处理
        private void LinkServerAckEvent(bool Success, int Interval)
        {

            try
            {
                _LinkServerSuccess = Success;
                if (Success)
                {
                    StartDetectSayHello(Interval);
                    CreateSayHelloTask();
                    if (CurLoginUser != null)
                    {
                        JLog.Instance.AppInfo("重登录");
                        UserReLoginREQ(_CurLoginUser);
                    }
                }

            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }



        }

        protected void SubDoUserLoginAckEvent(bool Succes, LoginUser User, string Msg)
        {
            try
            {
                if (Succes)
                {
                    CurLoginUser = User;
                    JLog.Instance.AppInfo(string.Format("{0}","登录成功"));
                }
                else
                {
                    JLog.Instance.AppInfo(string.Format("{0}", "登录失败"));
                }
                

            }
            catch (Exception ex)
            {
                JLog.Instance.Error(ex.Message, MethodBase.GetCurrentMethod().Name,
                    MethodBase.GetCurrentMethod().Module.Name);
            }
        }

        protected void SubDoSayHelloToClient()
        {
            IncrementServerSayHello();
        }
        #endregion

        #region 自定义事件

        #region 用户登录应答

        protected  degUserLoginAckHandle UserLoginAckEvent;

        protected void SubDoUserLoginAck(bool Succes, LoginUser User, string Msg)
        {
            RaiseUserLoginAckEvent( Succes,  User,  Msg);
        }

        protected void RaiseUserLoginAckEvent(bool Succes, LoginUser User, string Msg)
        {
            if (UserLoginAckEvent != null)
            {
                UserLoginAckEvent.BeginInvoke( Succes,  User,  Msg,null, null);
            }

        }
        /// <summary>
        /// 用户登录应答事件
        /// </summary>
        /// <param name="handle"></param>
        public void RegistUserLoginAckEvent(degUserLoginAckHandle handle)
        {
            UserLoginAckEvent = handle;


        }

        public void DeRegistUserLoginAckEvent(degUserLoginAckHandle handle)
        {
            UserLoginAckEvent = null;

        }
        #endregion





        #endregion

    }
}
