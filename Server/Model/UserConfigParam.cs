using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace NetPlan.Model
{

    /// <summary>
    /// 用户配置信息
    /// </summary>
    [Serializable]
    public class UserConfigParam
    {
        /// <summary>
        /// EDS　AutoLoad程序所在目录
        /// </summary>
        [XmlElement("程序文件路径")] 
        public string EDSLoadAppFile = string.Empty;
        /// <summary>
        /// 保存导入EDSXML文件存放的目录　
        /// </summary>
        [XmlElement("XML文件存放目录")]
        public string XmlFileSaveDir = string.Empty;
        /// <summary>
        /// 天线号
        /// </summary>
        //[XmlElement(ElementName = "天线型号",Order = 0)]
       [XmlArrayAttribute("可选天线型号")]
        public  List<string> AntennaTypes = new List<string>();
        [XmlArrayAttribute("可选载波")]
        public List<MCarrierItem> CarrierItems = new List<MCarrierItem>();
        [XmlArrayAttribute("工程列表")]
        public List<MProjectName> ProjectNames = new List<MProjectName>();
        [XmlArrayAttribute("传播模型")]
        public List<string>  ModelTypes = new List<string>();
        [XmlElement("EDS数据库参数")]
        public EDSConnectDB EDSDBInfo = new EDSConnectDB();
        [XmlElement("EAWSSchemaName")]
        public string EAWSSchemaName = string.Empty;
        /// <summary>
        /// 仿真保存目录
        /// </summary>
        [XmlElement("仿真结果目录")]
        public string EAWSRealseDir = string.Empty;
        /// <summary>
        /// FTP保存目录
        /// </summary>
        [XmlElement("FTP保存目录")]
        public string FTPDir = string.Empty;

        public  UserConfigParam()
        {
        }
    }


    /// <summary>
    /// 载波选择项
    /// </summary>
    public class MCarrierItem
    {
        [XmlAttribute("别名")]
        public string Alias = string.Empty;
        [XmlAttribute("编号")]
        public int ID = 0;
    }
    /// <summary>
    /// 工程名称
    /// </summary>
    public class MProjectName
    {
        [XmlAttribute("所属城市")]
        public string CityName = string.Empty;
        [XmlAttribute("工程名称")]
        public string ProjectName = string.Empty;
        [XmlAttribute("城市英文")]
        public string CityEName = string.Empty;
        [XmlAttribute("投影带号")]
        public int UtmID = 50;
        [XmlAttribute("仿真任务名")]
        public string TaskName = string.Empty;

    }
}
