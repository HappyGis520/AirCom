using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using JLIB.CSharp;
using JLIB.Utility;


namespace NetPlan.Model
{
     public    class GlobalInfo:Singleton<GlobalInfo>
     {
         public GlobalInfo()
         {
            JLog.Instance.AppInfo("反序列化配置文件");


#if WEB
            ConfigParam =  JFileExten.FromXML<UserConfigParam>(HttpContext.Current.Server.MapPath(@"~/AppConfig.xml"));
#else
                        ConfigParam = JFileExten.FromXML<UserConfigParam>(@".\AppConfig.xml");
#endif
        }

        public Hashtable JobsRunning = null;
        private string _XMLSavePath = string.Empty;
         /// <summary>
         /// Xml文件存储位置
         /// </summary>
         public string XmlSavePath
         {
             get { return _XMLSavePath; }
             set { _XMLSavePath = value; }
         }

         public UserConfigParam ConfigParam = null;





     }
}
