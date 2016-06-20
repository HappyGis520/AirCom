using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.ServiceModel.Channels;
using System.IO;

namespace ENSConsoleApp.ENSMessageReceiver
{
    // NOTE: If you change the class name "Service1" here, you must also update the reference to "Service1" in App.config.
    public class ENSMessageSubscriber : IENSMessageSubscriber
    {

        // Simple ENS implementation just takes raw message (not used in demo)
        public void NotificationMessage(Message msg)
        {            
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                NewLineHandling = NewLineHandling.Entitize
            };
            using (XmlWriter xw = XmlWriter.Create(sb, settings))
            {                
                XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateDictionaryWriter(xw);
                msg.WriteBodyContents(xdw);
                xdw.Flush();
            }
            Console.WriteLine(sb.ToString());
        }                        
    }   
}
