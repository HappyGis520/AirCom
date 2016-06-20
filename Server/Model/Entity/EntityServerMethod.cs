/*******************************************************************
 * * 版权所有： 王建军
 * * 文件名称： EntityServerMethod.cs
 * * 功   能：  
 * * 作   者： 王建军
 * * 编程语言： C# 
 * * 电子邮箱： 595303122@qq.com
 * * 创建日期： 2016-02-23 09:27:27
 * * 修改记录： 
 * * 日期时间： 2016-02-23 09:27:27  修改人：王建军  创建
 * *******************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPlan.Model
{
    internal class EntityServerMethod:IEntityMethod
    {

        public EtPlanTask GetEtPlanTaskByID(int ID)
        {
            return new EtPlanTask(ID);
        }
        public EtCellInfo GetEtCellInfoByID(int ID)
        {
            return new EtCellInfo(ID);
        }


        public EtProjectInfo GetEtProjectInfoByID(int ID)
        {
            return new EtProjectInfo(ID);
        }
        public EtBaseStation GetEtBaseStationByID(int ID)
        {
            return new EtBaseStation(ID);
        }
        public EtUser GetEtUserByID(int ID)
        {
            return new EtUser(ID);
        }
    }
}
