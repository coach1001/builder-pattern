using System;
using System.Collections.Generic;
using System.Text;
using CoreDui.Definitions;

namespace CoreDui.Persistance
{
    public interface IFlowPersistance
    {
        object GetFlow(string FlowId);
        void SetFlow(string FlowId, object FlowData);
    }
}
