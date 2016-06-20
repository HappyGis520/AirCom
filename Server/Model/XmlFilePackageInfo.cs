using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPlan.Model
{
    /// <summary>
    /// 导入EDS的xml文件包信息
    /// </summary>
     public  class XmlFilePackageInfo
    {
        /// <summary>
        /// Location文件全名
        /// </summary>
        public string InputLocationFileFullName = string.Empty;
        /// <summary>
        /// 导入的LTE基站文件全名
        /// </summary>
        public string InputLTENodeFileFullName = string.Empty;
        /// <summary>
        /// 删除的LTE基站文件全名
        /// </summary>
        public string DeleteLTENodeFileFullName = string.Empty;
        /// <summary>
        /// 删除的Location文件全名
        /// </summary>
        public string DeleteLocationFileFullName = string.Empty;
    }
}
