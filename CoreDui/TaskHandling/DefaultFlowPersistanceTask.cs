using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.Persistance;

namespace CoreDui.TaskHandling
{
    public class DefaultFlowPersistanceTask<TFlowDataType, TContextType> : IFlowTask<TFlowDataType, TContextType> 
        where TContextType : BaseContextModel
    {
        private readonly IFlowPersistance _flowPersistance;

        public DefaultFlowPersistanceTask(IFlowPersistance flowPersistance)
        {
            _flowPersistance = flowPersistance;
        }

        public Task<TaskData<TFlowDataType, TContextType>> Execute(TaskData<TFlowDataType, TContextType> taskData)
        {
            _flowPersistance.SetFlow(taskData.Context.FlowId.ToString(), taskData);
            return Task.FromResult(taskData);
        }

    }
}
