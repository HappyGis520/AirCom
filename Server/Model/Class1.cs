using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace NetPlan.Model
{
    //    //说明：开始时准备设计成SerializableDictionary<U, T>，
    //    //后来发现在多数情况下的key只需要是字符串就足够了，所以就不理会U的类型，
    //    //直接使用默认的string了。
    //    public class XmlSerialDictionary<T> :
    //        Dictionary<int, T>, //继承Dictionary
    //        System.Xml.Serialization.IXmlSerializable //要能够序列化，必须实现IXmlSerializable接口 
    //    {
    //        public System.Xml.Schema.XmlSchema GetSchema()
    //        {
    //            return null;
    //        }

    //        //从xml文件中读取内容并转换为对象
    //    public void ReadXml(System.Xml.XmlReader reader)
    //        {
    //            if (reader.IsEmptyElement)
    //                return;
    //            Type type = typeof(T);
    //            PropertyInfo[] pis = type.GetProperties(); //获取类型T中的所有属性信息
    //            reader.Read();
    //            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
    //            {
    //                //当前节点的限定名。例如，对于元素 <bk:book>，Name 为 bk:book。
    //                int key =int.Parse(reader.Name);

    //                //调用类型的默认构造函数创建类型T的实例，并作为value值写入Dictionary对象中
    //                this[key] = (T)type.GetConstructor(new Type[] { }).Invoke(null);
    //                if (!reader.IsEmptyElement)
    //                {
    //                    reader.ReadStartElement();
    //                    if (reader.NodeType != System.Xml.XmlNodeType.EndElement)
    //                    {
    //                        foreach (PropertyInfo pi in pis)
    //                        {
    //                            pi.SetValue(this[key],
    //                                //根据属性的类型对读取出来的字符串进行转换
    //                                Convert.ChangeType(reader.ReadElementString(pi.Name), pi.PropertyType), null);
    //                        }
    //                    }
    //                }
    //                reader.Read();
    //            }
    //        }
    //        public void WriteXml(System.Xml.XmlWriter writer)
    //        {
    //            Type type = typeof(T);
    //            //获取对象的全部属性信息
    //            PropertyInfo[] pis = type.GetProperties();
    //            foreach (int key in this.Keys)
    //            {
    //                writer.WriteStartElement(key.ToString());
    //                //将对象的每一个属性名和属性值写入xml文件中
    //                foreach (PropertyInfo pi in pis)
    //                {
    //                    //<属性名>属性值</属性名>
    //                    //<pi.Name>pi.GetValue(this[key], null).ToString()</pi.Name>
    //                    writer.WriteElementString(pi.Name, pi.GetValue(this[key], null).ToString());
    //                }
    //                writer.WriteEndElement();
    //            }
    //        }
    //}
    public class SerializableHashtable : System.Collections.Hashtable, IXmlSerializable
    {
        #region IXmlSerializable 成员

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }


        private XmlSerializer serializer = new XmlSerializer(typeof(object));



        public void ReadXml(System.Xml.XmlReader reader)
        {
            bool isEmpty = reader.IsEmptyElement;
            reader.Read();
            if (isEmpty)
                return;
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                object key = serializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                object value = serializer.Deserialize(reader);
                reader.ReadEndElement();
                this[key] = value;
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();

        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (object key in this.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                serializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                object value = this[key];
                serializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }

        }

        #endregion
    }

    /// <summary>
    /// Dictionary(支持 XML 序列化)
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    [XmlRoot("XmlDictionary")]
    public class XmlSerialDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region 构造函数

        public XmlSerialDictionary() : base() { }
        public XmlSerialDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        public XmlSerialDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
        public XmlSerialDictionary(int capacity) : base(capacity) { }
        public XmlSerialDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
        protected XmlSerialDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion 构造函数

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// 从对象的 XML 表示形式生成该对象(反序列化)
        /// </summary>
        /// <param name="xr"></param>
        public void ReadXml(XmlReader xr)
        {
            if (xr.IsEmptyElement) return;
            var ks = new XmlSerializer(typeof(TKey));
            var vs = new XmlSerializer(typeof(TValue));
            xr.Read();
            while (xr.NodeType != XmlNodeType.EndElement)
            {
                xr.ReadStartElement("Item");
                xr.ReadStartElement("Key");
                var key = (TKey)ks.Deserialize(xr);
                xr.ReadEndElement();
                xr.ReadStartElement("Value");
                var value = (TValue)vs.Deserialize(xr);
                xr.ReadEndElement();
                this.Add(key, value);
                xr.ReadEndElement();
                xr.MoveToContent();
            }
            xr.ReadEndElement();
        }

        /// <summary>
        /// 将对象转换为其 XML 表示形式(序列化)
        /// </summary>
        /// <param name="xw"></param>
        public void WriteXml(XmlWriter xw)
        {
            var ks = new XmlSerializer(typeof(TKey));
            var vs = new XmlSerializer(typeof(TValue));
            foreach (var key in this.Keys)
            {
                xw.WriteStartElement("Item");
                xw.WriteStartElement("Key");
                ks.Serialize(xw, key);
                xw.WriteEndElement();
                xw.WriteStartElement("Value");
                vs.Serialize(xw, this[key]);
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
        }

        #endregion IXmlSerializable Members
    }
}

