using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using JLIB.Utility;
using NetPlan.IContract;
using NetPlan.Model;

namespace NetPlan.Servcie
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public partial class AircomServcie : IAircomServcie
    {
        public bool CreateTask(PLAData Data)
        {
            JLog.Instance.AppInfo("收到请求.....");
            return true;
        }

        public bool Hello()
        {
            JLog.Instance.AppInfo("收到请求hello.....");
            return true;
        }
    }
}
