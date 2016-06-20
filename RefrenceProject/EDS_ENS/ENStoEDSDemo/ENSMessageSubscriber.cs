using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.ServiceModel.Channels;
using System.IO;
using System.Net;

namespace ENStoEDSDemo
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in App.config.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] 
    public class ENSMessageSubscriber : IENSMessageSubscriber, IDisposable
    {
        static EDSMessageDispatcher dispatcher = new EDSMessageDispatcher();
        AutoSubscriptionManager mgr;

        private static IPAddress GetHostIPAddress()
        {
            string sHost = Dns.GetHostName();
            var ipv = Dns.GetHostAddresses(sHost).Where(ip =>
                                                        ip.IsIPv6LinkLocal == false &&
                                                        ip.IsIPv6Multicast == false &&
                                                        ip.IsIPv6SiteLocal == false &&
                                                        ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                                                        ip.ToString().Substring(0, 3) != "169").FirstOrDefault();
            return ipv;
        }

        public ENSMessageSubscriber()
        {
            var ipv = GetHostIPAddress();
            Uri uri = new Uri(string.Format("http://{0}:8731/ENSDemo/MessageReceiver/", ipv.ToString()));
            mgr = new AutoSubscriptionManager(uri.AbsoluteUri);
        }

        // Simple ENS implementation just takes raw message (not used in demo)
        public void NotificationMessage(Message msg)
        {            
            StringBuilder sb = new StringBuilder();
            using (XmlWriter xw = XmlWriter.Create(sb))
            {                
                XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateDictionaryWriter(xw);
                msg.WriteBodyContents(xdw);
                xdw.Flush();
            }
            System.Diagnostics.Debug.WriteLine(sb.ToString());
        }

        // Responds to ENS message, use's deserialized item data and creates basic EDS query...
        public void Notification(EventSourceNotificationMessage data)
        {
            try
            {          
                // Forward ENS to request to STATIC EDS connection - be wary of threads...
                dispatcher.RequestData(data.NotificationData);
            }
            catch (Exception e)
            {
                System.Diagnostics.EventLog.WriteEntry("EDStoENSDemo", e.ToString());
            }
        }



        #region IDisposable Members

        public void Dispose()
        {
            mgr.Dispose();
        }

        #endregion
    }
}
