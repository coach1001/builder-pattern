using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDui.Persistance
{
    public class InMemoryFlowPersistance : IFlowPersistance
    {
        private Dictionary<string, object> FlowDataRepository { get; set; } = new Dictionary<string, object>();

        public object GetFlow(string flowId)
        {
            if(FlowDataRepository.TryGetValue(flowId, out var flowData))
            {
                return flowData;
            }
            else
            {
                return null;
            }
        }

        public void SetFlow(string flowId, object flowData)
        {
            FlowDataRepository[flowId] = flowData;
        }
    }
}
