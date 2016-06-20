using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPlan.Model
{
     public interface IEntityMethod
     {
         EtPlanTask GetEtPlanTaskByID(int ID);

         EtCellInfo GetEtCellInfoByID(int ID);
         EtProjectInfo GetEtProjectInfoByID(int ID);

         EtBaseStation GetEtBaseStationByID(int ID);
         EtUser GetEtUserByID(int ID);
     }
}
