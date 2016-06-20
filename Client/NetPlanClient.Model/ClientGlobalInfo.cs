using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JLIB.CSharp;
using NetPlan.Model;

namespace NetPlanClient.Model
{
     public    class ClientGlobalInfo:Singleton<ClientGlobalInfo>
     {
        
         public ClientGlobalInfo()
         {
           ConfigParam =  JFileExten.FromXML<UserConfigParam>(".\\AppConfig.xml");
         }


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
