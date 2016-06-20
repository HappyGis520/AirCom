using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ENSConsoleApp.ENSMessageReceiver;
using System.Xml;
using System.Net;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.ServiceModel.Channels;

namespace ENSConsoleApp
{   
    class Program
    {       
        static void Main(string[] args)
        {   
            Uri uri = new Uri(string.Format("http://{0}:1234/ENSListener", Dns.GetHostName()));

            // Host a local service instance to receive ens messages
            using (ServiceHost svc = new ServiceHost(typeof(ENSMessageSubscriber), uri))
            {
                svc.Open();

                try
                {
                    ENSProxy.Identifier subscriptionId = null;

                    using (ENSProxy.EventSourceClient eventSourceManager = new ENSProxy.EventSourceClient())
                    {
                        //Test Subscribe
                        ENSProxy.Subscribe subscribe = new ENSProxy.Subscribe()
                        {
                            Delivery = new ENSProxy.DeliveryType()
                            {
                                NotifyTo = new ENSProxy.EndpointReferenceType()
                                {
                                    Address = new ENSProxy.AttributedURI()
                                    {
                                        Value = uri.AbsoluteUri
                                    }
                                }
                            },
                            Expires = new ENSConsoleApp.ENSProxy.ExpirationType()
                            {
                                Value = DateTime.Now.AddHours(4.0).ToUniversalTime()
                            }
                        };

                        ENSProxy.SubscribeResponse response = eventSourceManager.SubscribeOp(subscribe);
                        subscriptionId = response.SubscriptionManager.ReferenceParameters.Identifier;
                        Console.WriteLine("Subscription created - " + subscriptionId.Id);
                    }

                    if (subscriptionId != null)
                    {
                        using (ENSProxy.SubscriptionManagerClient subManager = new ENSProxy.SubscriptionManagerClient())
                        {
                            //Test Get Status
                            ENSProxy.GetStatusResponse statusResponse = subManager.GetStatusOp(subscriptionId, new ENSProxy.GetStatus());
                            Console.WriteLine("Subscription status  - current expiry is " + statusResponse.Expires.Value.ToLocalTime().ToString());

                            //Test Renew
                            ENSProxy.Renew renew = new ENSProxy.Renew()
                            {
                                Expires = new ENSProxy.ExpirationType()
                                {
                                    Value = DateTime.Now.AddHours(6.0).ToUniversalTime()
                                },
                            };

                            ENSProxy.RenewResponse renewResponse = subManager.RenewOp(subscriptionId, renew);
                            Console.WriteLine("Subscription renewed - new expiry is " + renewResponse.Expires.Value.ToLocalTime().ToString());

                            //Test Get Subscription List
                            ENSProxy.SubscriptionsListResponse listResponse = subManager.GetSubscriptionListOp(new ENSProxy.SubscriptionsList());
                            if (listResponse.Subscriptions.Count() > 0)
                            {
                                Console.WriteLine("Subscriptions:");
                                foreach (ENSProxy.Subscription subscription in listResponse.Subscriptions)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Id:      " + subscription.Manager.Identifier.Id);
                                    Console.WriteLine("Expires: " + subscription.Expires.ToLocalTime().ToString());
                                }
                                Console.WriteLine();
                            }
                            else
                                Console.WriteLine("No Subscriptions present at this time");

                            //Test Unsubscribe
                            subManager.UnsubscribeOp(subscriptionId, new ENSProxy.Unsubscribe());
                            Console.WriteLine("Subscription deleted - " + subscriptionId.Id);
                        }
                    }
                }
                catch (FaultException ex)
                {
                    Console.WriteLine("Contract Exception: " + ex.Message);
                }
                catch (CommunicationException ex)
                {
                    Console.WriteLine("Communication Exception: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown Exception: " + ex.Message);
                }
            }
        }
    }
}
