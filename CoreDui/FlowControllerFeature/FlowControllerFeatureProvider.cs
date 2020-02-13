using System.Collections.Generic;
using CoreDui.Repositories;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using System.Reflection;
using CoreDui.Definitions;
using System;
using Microsoft.AspNetCore.Authorization;

namespace CoreDui.FlowControllerFeature
{
    public class FlowControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly IModuleRepository _moduleRepositoryDelegate;
        public FlowControllerFeatureProvider(IModuleRepository moduleRepositoryDelegate)
        {
            _moduleRepositoryDelegate = moduleRepositoryDelegate;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {

            foreach (var module in _moduleRepositoryDelegate.GetModules())
            {
                var routeModule = $"api/{module.System}/{module.Module}";

                if (!feature.Controllers.Any(t => t.Name == routeModule))
                {
                    var moduleControllerTypeInfo = typeof(ModuleController<>).MakeGenericType(typeof(object)).GetTypeInfo();
                    var moduleDelegateInfo = new ModuleDelegationType
                    {
                        System = module.System,
                        Module = module.Module,
                        ModuleDefinition = module,
                        Route = routeModule
                    };
                    var moduleDelegator = new ModuleTypeDelegator<ModuleDelegationType>(moduleControllerTypeInfo, moduleDelegateInfo);
                    feature.Controllers.Add(moduleDelegator);
                }

                foreach (var flow in module.Flows)
                {
                    var route = $"api/{module.System}/{module.Module}/{flow.Flow}";

                    if (!feature.Controllers.Any(t => t.Name == route))
                    {
                        var controllerTypeInfo = typeof(FlowController<,>).MakeGenericType(flow.DataType, flow.ContextType).GetTypeInfo();
                        var delegateInfo = new FlowDelegationType
                        {
                            System = module.System,
                            Module = module.Module,
                            FlowDefinition = flow,
                            Route = route
                        };
                        var delegator = new FlowTypeDelegator<FlowDelegationType>(controllerTypeInfo, delegateInfo);
                        feature.Controllers.Add(delegator);
                    }
                }
            }
        }
    }
}
