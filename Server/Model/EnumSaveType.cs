using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NetPlan.Model
{
    /// <summary>
    /// 执行分析后是否保留
    /// </summary>
    public  enum EnumSaveType
    {
        [Description("保留")]
        Save,
        [Description("删除")]
        Delete
    }
}
