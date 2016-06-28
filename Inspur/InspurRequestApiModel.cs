using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Inspur
{
    [Serializable][XmlRoot("req")]
    public class InspurRequestApiModel
    {
        /// <summary>
        /// 对应工单流水号
        /// </summary>
        public Int64 flow_id { get; set; }
        /// <summary>
        /// 生成结果的压缩包名称（唯一）
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
