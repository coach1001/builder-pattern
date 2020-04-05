using Autofac;
using CoreDui.Definitions;
using CoreDui.Enums;
using CoreDui.TaskHandling;
using CoreDui.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CoreDui.FlowControllerFeature
{
    [Authorize]
    [ApiController]    
    [FlowControllerRoute]
    [Route("[controller]")]    
    public class AuthorizedFlowController<TFlowDataType, TContextType> : ControllerBase
    {
        private readonly ILifetimeScope _scope;

        public AuthorizedFlowController(ILifetimeScope scope)
        {
            _scope = scope;
        }
        
        [HttpPost("run-task")]
        public async Task<TaskData<TFlowDataType, TContextType>> PostTaskAsync([FromBody] TaskData<TFlowDataType, TContextType> taskData)
        {   
            
            var flow = (FlowDelegationType)ControllerContext.ActionDescriptor.Properties
                .FirstOrDefault(x => x.Key.ToString() == "Flow").Value;

            //Return 403 when not permitted
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(m => m.Value);
            
            if (ModelState.IsValid)
            {

            }

            var tasks = TaskSearch.Search(flow.FlowDefinition, taskData.TaskPath, taskData.TaskType);

            foreach(var task in tasks)
            {
                if (task.ExecuteOn == ExecuteOn.Api)
                {
                    var taskToExecute = (IFlowTask<TFlowDataType, TContextType>)_scope.Resolve(task.Type);
                    taskData = await taskToExecute.Execute(taskData);
                }
            }

            return taskData;
        }

    }


}
