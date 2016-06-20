using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetPlan.Model
{
    [Serializable]
     public   class CellSector
     {
         public  int CellID =13777;
        public List<AirComAntennaType>  Antenners = new List<AirComAntennaType>();
     }
}
