using CoreDui.Definitions;
using CoreDui.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace CoreDui.FlowControllerFeature
{        
    [ApiController]    
    [FlowControllerName]
    [Route("[controller]")]
    public class FlowController<TFlowDataType, TContextType> : ControllerBase
    {
        [HttpGet("definition")]
        public IActionResult IndexAsync()
        {
            var flow = 
                (FlowDelegationType) ControllerContext.ActionDescriptor.Properties
                .FirstOrDefault(x => x.Key.ToString() == "Flow").Value;
            
            return Ok(flow);
        }

        [HttpPost("run-task")]
        public async System.Threading.Tasks.Task<IActionResult> PostTaskAsync([FromBody] TaskData<TFlowDataType, TContextType> taskData)
        {           
            if(ModelState.IsValid)
            {

            }

            var flow =
                (FlowDelegationType)ControllerContext.ActionDescriptor.Properties
                .FirstOrDefault(x => x.Key.ToString() == "Flow").Value;

            var tasks = TaskSearch.Search(flow.Flow, taskData.TaskPath, taskData.TaskType);
            foreach(var task in tasks)
            {
                taskData = await task.Run(taskData);
            }

            return Ok(taskData);
        }

    }


}
