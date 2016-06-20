using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JLIB.CSharp;

namespace NetPlan.Model
{
     public    class GlobalInfo:Singleton<GlobalInfo>
     {
         public GlobalInfo()
         {
           ConfigParam =  JFileExten.FromXML<UserConfigParam>(".\\AppConfig.xml");
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
