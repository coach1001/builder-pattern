using CoreDui.Definitions;
using CoreDui.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace CoreDui.FlowControllerFeature
{
    [ApiController]
    [ModuleControllerRoute]
    [Route("[controller]")]
    public class ModuleController<TData> : ControllerBase
    {
        [HttpGet("definition")]
        public IActionResult IndexAsync()
        {
            var module = 
                (ModuleDelegationType)ControllerContext.ActionDescriptor.Properties
                .FirstOrDefault(x => x.Key.ToString() == "Module").Value;

            return Ok(module.ModuleDefinition);
        }
    }
}
