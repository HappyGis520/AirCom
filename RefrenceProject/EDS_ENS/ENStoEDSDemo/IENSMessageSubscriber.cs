using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Serialization;

namespace ENStoEDSDemo
{
    public static class NamespaceTypes
    {
        public const string ns = "http://www.aircominternational.com/contract/ENS/Data/2009/10";
    }

    
    [XmlRoot(Namespace = NamespaceTypes.ns)]    
    public class NotificationData
    {
        public string ObjectID;

        public int ObjectKey;

        public string ObjectTypeString;

        public int ObjectType;

        public string Project;

        public string UserID;

        public DateTime TimeStamp;

        //public XmlElement CustomData;

        public XmlElement ExtensionData;
    }

    [MessageContract(IsWrapped = false)]
    public class EventSourceNotificationMessage
    {
        [MessageBodyMember(Namespace = NamespaceTypes.ns)]
        public NotificationData NotificationData;
        public EventSourceNotificationMessage() { }
        public EventSourceNotificationMessage(NotificationData data) { NotificationData = data; }
    }

    // NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in App.config.
    [ServiceContract]    
    public interface IENSMessageSubscriber 
    {
        //[OperationContract(Action = "NotificationMessage", IsOneWay = false, ReplyAction = "*")]
        //void NotificationMessage(Message msg);

        [OperationContract(Action = "NotificationMessage", IsOneWay = false, ReplyAction = "*")]
        [XmlSerializerFormat]
        void Notification(EventSourceNotificationMessage data);

    }

}
