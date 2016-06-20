using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPlan.Model
{
    /// <summary>
    /// 仿真范围
    /// </summary>
    [Serializable]
    public  class GeoRegion
    {

        public double EastMin = 0;
        public double Eastmax = 120;
        public double NorthMin =0;
        public double NorthMax = 80;
    }
}
