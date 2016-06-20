using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JLIB.CSharp;
using NetPlan.BDL;
using NetPlan.Model;

namespace NetPlan.BLL
{
     internal class BLLUser:Singleton<BLLUser>
     {
         internal EtUser GetUser(string UserName, string Password)
         {
             var objs = BDLUser.GetUserByUserInfo(UserName, Password);
             if (objs != null && objs.Count > 0)
             {
                 return objs.First();
             }
             return null;
         }
    }
}
