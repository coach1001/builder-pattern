﻿using Autofac;
using CoreDui.Definitions;
using CoreDui.Enums;
using CoreDui.TaskHandling;
using CoreDui.Utils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDui.FlowControllerFeature
{        
    [ApiController]    
    [FlowControllerRoute]
    [Route("[controller]")]
    public class FlowController<TFlowDataType, TContextType> : ControllerBase
    {
        private readonly ILifetimeScope _scope;

        public FlowController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        [HttpPost("run-task")]
        public async Task<TaskData<TFlowDataType, TContextType>> PostTaskAsync([FromBody] TaskData<TFlowDataType, TContextType> taskData)
        {           
            if(ModelState.IsValid)
            {

            }
            var flow = (FlowDelegationType)ControllerContext.ActionDescriptor.Properties
                .FirstOrDefault(x => x.Key.ToString() == "Flow").Value;

            var tasks = TaskSearch.Search(flow.FlowDefinition, taskData.TaskPath, taskData.TaskType);

            foreach(var task in tasks)
            {
                if(task.ExecuteOn == ExecuteOn.Api)
                {
                    var taskToExecute = (IFlowTask<TFlowDataType, TContextType>)_scope.Resolve(task.Type);
                    taskData = await taskToExecute.Execute(taskData);
                }
            }

            return taskData;
        }

    }


}
