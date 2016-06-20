using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NetPlan.Model
{
    /// <summary>
    /// 基站类别
    /// </summary>
    public enum EnumStationType
    {
       /// <summary>
        /// 规划站
       /// </summary>
        [Description("规划站")]
        Planning,
        /// <summary>
        /// 现有站
        /// </summary>
        [Description("现有站")]
        Extant,
        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        Other
    }
}
