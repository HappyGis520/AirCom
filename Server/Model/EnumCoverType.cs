using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NetPlan.Model
{
    /// <summary>
    /// 覆盖类别
    /// </summary>
    public enum  EnumCoverType
    {
        /// <summary>
        /// 室外
        /// </summary>
        [Description("室外")]
        Outdoor,
        /// <summary>
        /// 室内
        /// </summary>
        [Description("室内")]
        Indoor
    }
}
