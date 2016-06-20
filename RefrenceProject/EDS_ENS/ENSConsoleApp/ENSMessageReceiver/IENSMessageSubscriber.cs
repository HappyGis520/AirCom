using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Serialization;

namespace ENSConsoleApp.ENSMessageReceiver
{
    public static class NamespaceTypes
    {
        public const string ns = "http://www.aircominternational.com/contract/ENS/Data/2009/10";
    }

    
    [DataContract(Namespace = NamespaceTypes.ns, Name = "NotificationData")]    
    public class NotificationData
    {
        [DataMember(Name = "ObjectID", Order = 1)]
        public string ID;

        [DataMember(Name = "ObjectKey", Order = 2)]
        public int Key;

        [DataMember(Name = "ObjectTypeString", Order = 3)]
        public string Type;

        [DataMember(Name = "ObjectType", Order = 4)]
        public int OrdinalType;

        [DataMember(Name = "Project", Order = 5)]
        public string Project;

        [DataMember(Name = "UserID", Order = 6)]
        public string User;

        [DataMember(Name = "TimeStamp", Order = 7, IsRequired = false)]
        public DateTime TimeStamp;

        [DataMember(Name = "CustomData", Order = 8)]
        public XmlElement Custom;

        [DataMember(Name = "ExtensionData", Order = 9)]
        public XmlElement Extension;
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
        [OperationContract(Action = "NotificationMessage", IsOneWay = false, ReplyAction = "*")]
        void NotificationMessage(Message msg);

        //[OperationContract(Action = "NotificationMessage", IsOneWay = false, ReplyAction = "*")]
        //void Notification(EventSourceNotificationMessage data);

    }

}
