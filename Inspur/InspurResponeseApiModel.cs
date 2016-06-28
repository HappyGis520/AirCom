using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Inspur
{
    [Serializable]
    [XmlRoot("req")]
    public class InspurResponeseApiModel
    {
        /// <summary>
        /// -1代表接受失败,0接受成功,
        /// </summary>
        public string is_archive { get; set; }
        /// <summary>
        /// 如：success；false
        /// </summary>
        public string remark { get ; set; }
    }
}
