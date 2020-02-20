using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.Enums;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.UserRegistration
{
    public class UserRegistrationTask : IFlowTask<UserRegistrationModel, UserRegistrationContext>
    {

        private readonly DbLabCalcContext _context;

        public UserRegistrationTask(DbLabCalcContext context)
        {
            _context = context;
        }

        public Task<TaskData<UserRegistrationModel, UserRegistrationContext>> Execute(TaskData<UserRegistrationModel, UserRegistrationContext> data)
        {            
            data.Context.UpdatedAt = DateTime.UtcNow;
            return Task.FromResult(data);
        }
    }
}
