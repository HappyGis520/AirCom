using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;
using System.ServiceModel.Channels;
using ENStoEDSDemo.ENSServiceProxy;

namespace ENStoEDSDemo
{
    /// <summary>
    /// Simple wrapper class around the subscription manager - automatically takes subscribes the client ENS endpoint to the 
    /// backend subscription manager
    /// </summary>
    public class AutoSubscriptionManager : IDisposable
    {
        Identifier _ENSClientID = null;
        Uri _LocalENSSubscriber;

        /// <summary>
        /// Creates a new instance of this type - implements IDisposable
        /// </summary>
        /// <param name="sSubscriptionManagerEndpoint">Remote subscriptionmanager end point e.g. http://myserver:8833/ENS/SubscriptionManager </param>
        /// <param name="LocalENSSubscriber">Local ENS receiving endpoint e.g. http://myclient:8731/ENSDemo/MessageReceiver/ </param>
        public AutoSubscriptionManager(string LocalENSSubscriber)
        {
            this._LocalENSSubscriber = new Uri(LocalENSSubscriber);
            Subscribe();
        }

        public void Dispose()
        {
            UnSubscribe();
        }

        /// <summary>
        /// Takes the endpoint detail and creates a subscription
        /// </summary>
        void Subscribe()
        {
            try
            {
                using (ENSServiceProxy.EventSourceClient eventSourceManager = new ENSServiceProxy.EventSourceClient())
                {
                    ENSServiceProxy.Subscribe subscribe = new ENSServiceProxy.Subscribe()
                    {
                        Delivery = new ENSServiceProxy.DeliveryType()
                        {
                            NotifyTo = new ENSServiceProxy.EndpointReferenceType()
                            {
                                Address = new ENSServiceProxy.AttributedURI()
                                {
                                    Value = this._LocalENSSubscriber.AbsoluteUri
                                }
                            }
                        }
                    };

                    ENSServiceProxy.SubscribeResponse response = eventSourceManager.SubscribeOp(subscribe);
                    _ENSClientID = response.SubscriptionManager.ReferenceParameters.Identifier;
                    System.Diagnostics.Debug.WriteLine("Subscription created - " + _ENSClientID);
                }
            }
            catch (FaultException ex)
            {
                System.Diagnostics.Debug.WriteLine("Contract Exception: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine("Communication Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unknown Exception: " + ex.Message);
            }
        }

        void UnSubscribe()
        {
            try
            {
                using (ENSServiceProxy.SubscriptionManagerClient subscriptionManager = new ENSServiceProxy.SubscriptionManagerClient())
                {
                    subscriptionManager.UnsubscribeOp(_ENSClientID, new Unsubscribe());
                    System.Diagnostics.Debug.WriteLine("Subscription deleted - " + _ENSClientID);
                }
            }
            catch (FaultException ex)
            {
                System.Diagnostics.Debug.WriteLine("Contract Exception: " + ex.Message);
            }
            catch (CommunicationException ex)
            {
                System.Diagnostics.Debug.WriteLine("Communication Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unknown Exception: " + ex.Message);
            }
        }
    }
}
