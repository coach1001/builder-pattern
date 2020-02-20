using Autofac;
using CoreDui.Definitions;
using CoreDui.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Linq;


namespace CoreDui.FlowControllerFeature
{
    [ApiController]
    [ModuleControllerRoute]
    [Route("[controller]")]
    public class ModuleController<TData> : ControllerBase
    {
        private readonly ILifetimeScope _scope;

        public ModuleController(ILifetimeScope scope)
        {
            _scope = scope;
        }

        [HttpGet("definition")]
        public IActionResult IndexAsync()
        {
            _scope.GetType();

            var module = 
                (ModuleDelegationType)ControllerContext.ActionDescriptor.Properties
                .FirstOrDefault(x => x.Key.ToString() == "Module").Value;

            return Ok(module.ModuleDefinition);
        }
    }
}
