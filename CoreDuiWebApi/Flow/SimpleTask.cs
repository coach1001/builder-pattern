using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow
{
    public class SimpleTask : FlowTaskHandler
    {
        public override Task<TaskData<TFlowDataType, TContextType>> Run<TFlowDataType, TContextType>(TaskData<TFlowDataType, TContextType> taskData)
        {
            var data = taskData.Data as SimpleFlowModel;
            var context = taskData.Context as ContextModel;

            context.UpdatedAt = DateTime.Now.ToUniversalTime();

            return Task.FromResult(taskData);
        }
    }
}
