using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NetPlan.Model
{

    [Serializable]
    /// <summary>
    /// EDS数据库连接参数
    /// </summary>
    public  class EDSConnectDB
    {
        [XmlAttribute("用户名")]
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName ="Wangjj";

        /// <summary>
        /// 密码
        /// </summary>
        [XmlAttribute("密码")]
        public string Password = "123456";
        [XmlAttribute("Host")]
        public string Host = "127.0.0.1";
        /// <summary>
        /// 数据库名
        /// </summary>
        [XmlAttribute("SERVICE_NAME")]
        public string SERVICE_NAME = "XE";
    }
}
