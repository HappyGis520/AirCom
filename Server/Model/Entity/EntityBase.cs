/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： EntityBase.cs
 * * 功   能：  
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016-02-23 09:27:15
 * * 修改记录： 
 * * 日期时间： 2016-02-23 09:27:15  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPlan.Model
{
    [Serializable]
    public abstract  class  EntityBase
    {
        //从服务端获取数据的接口（一般为实体做的增、删、改、查）
        public static IEntityMethod Method = null;
        /// <summary>
        /// 加载引用类型的对象
        /// </summary>
        public abstract void LoadRefrenceObject();
    }
}
