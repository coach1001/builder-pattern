using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;
using CoreDui.Definitions;

namespace CoreDui.FlowControllerFeature
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ModuleControllerRouteAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.GetGenericTypeDefinition() == typeof(ModuleController<>))
            {
                var delegateInfo = (ModuleDelegationType) controller.Attributes.FirstOrDefault(x => x.GetType() == typeof(ModuleDelegationType));                
                controller.ControllerName = delegateInfo.Route;                
                controller.Properties.Add("Module", delegateInfo);
            }
        }
    }
}
