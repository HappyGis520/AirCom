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

        public int EastMin = 0;
        public int Eastmax = 120;
        public int NorthMin =0;
        public int NorthMax = 80;
    }
}
