﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;
using CoreDui.Definitions;

namespace CoreDui.FlowControllerFeature
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class FlowControllerRouteAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (
                controller.ControllerType.GetGenericTypeDefinition() == typeof(FlowController<,>)
                || controller.ControllerType.GetGenericTypeDefinition() == typeof(AuthorizedFlowController<,>))
            {
                var delegateInfo = (FlowDelegationType) controller.Attributes.FirstOrDefault(x => x.GetType() == typeof(FlowDelegationType));                
                controller.ControllerName = delegateInfo.Route;
                controller.Properties.Add("Flow", delegateInfo);
            }
        }
    }
}
