using CoreDui.Definitions;
using CoreDui.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace CoreDui.FlowControllerFeature
{        
    [ApiController]    
    [FlowControllerRoute]
    [Route("[controller]")]
    public class FlowController<TFlowDataType, TContextType> : ControllerBase
    {
        [HttpPost("run-task")]
        public async System.Threading.Tasks.Task<IActionResult> PostTaskAsync([FromBody] TaskData<TFlowDataType, TContextType> taskData)
        {           
            if(ModelState.IsValid)
            {

            }

            var flow =
                (FlowDelegationType)ControllerContext.ActionDescriptor.Properties
                .FirstOrDefault(x => x.Key.ToString() == "Flow").Value;

            var tasks = TaskSearch.Search(flow.FlowDefinition, taskData.TaskPath, taskData.TaskType);
            foreach(var task in tasks)
            {
                taskData = await task.Run(taskData);
            }

            return Ok(taskData);
        }

    }


}
