using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.UserRegistration
{
    public class UserRegistrationTask : FlowTaskHandler
    {
        public override Task<TaskData<TFlowDataType, TContextType>> Run<TFlowDataType, TContextType>(TaskData<TFlowDataType, TContextType> taskData)
        {
            var data = taskData.Data as UserRegistrationModel;
            var context = taskData.Context as UserRegistrationContext;

            context.UpdatedAt = DateTime.Now.ToUniversalTime();
            return Task.FromResult(taskData);
        }
    }
}
