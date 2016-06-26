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
        public override string ToString()
        {
           return  string.Format("EastMin{0}：Eastmax：{1}NorthMin：{2}NorthMax：{3}", EastMin, Eastmax, NorthMin, NorthMax);
        }
    }
}
