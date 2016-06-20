/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： EnumExcutEDSType.cs
 * * 功   能：  描述调用EDS Load程序的目的，
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2015-10-19 08:25:30
 * * 修改记录： 
 * * 日期时间： 2015-10-19 08:25:30  修改人：王建军  创建
 * *******************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;


namespace NetPlan.Model
{
    public enum EnumExcutEDSType
    {
        [Description("创建")]
        create,
        [Description("更新")]
        update,
        [Description("导出")]
        read



    }
}
