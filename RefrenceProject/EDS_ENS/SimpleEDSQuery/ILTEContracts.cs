using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleEDSQuery.EDSDemoProxy;

namespace SimpleEDSQuery
{
    public interface ILTEContracts
    {
         void DeleteLoations(List<RootEntityType> Locations);

         void DeleteLocation(RootEntityType Location);


         void CreateLocation(RootEntityType Location);
         void CreateLocation(List<RootEntityType> Location);


        void DeleteNode(LTENodeType Node);
        void DeleteNodes(List<LTENodeType> Nodes);
    }
}
