using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleEDSQuery.EDSDemoProxy;

namespace SimpleEDSQuery
{
    internal  class LTEServer:ILTEContracts
    {
        void ILTEContracts.DeleteLoations(List<RootEntityType> Locations)
        {
            throw new NotImplementedException();
        }

        void ILTEContracts.DeleteLocation(RootEntityType Location)
        {
            throw new NotImplementedException();
        }

        void ILTEContracts.CreateLocation(RootEntityType Location)
        {
            throw new NotImplementedException();
        }

        void ILTEContracts.CreateLocation(List<RootEntityType> Location)
        {
            throw new NotImplementedException();
        }

        void ILTEContracts.DeleteNode(LTENodeType Node)
        {
            throw new NotImplementedException();
        }

        void ILTEContracts.DeleteNodes(List<LTENodeType> Nodes)
        {
            throw new NotImplementedException();
        }
    }
}
