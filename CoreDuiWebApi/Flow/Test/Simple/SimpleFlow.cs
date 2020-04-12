using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Builders;
using CoreDui.Definitions;
using CoreDui.Enums;

namespace CoreDuiWebApi.Flow.Test
{
    public static class SimpleFlow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<SimpleData, SimpleContext>("simple-flow")
                    .WithStep(m => m.Step1)
                        .Next("Next")
                        .GridConfig("1fr")
                        .AddControl(m => m.Shower, ControlType.Boolean, "Toggle to show other value")
                        .End()
                        .AddControl(m => m.DependsOnShower, ControlType.Text, "Toggled Value")
                            .WithReactivity(m => m.Shower != true, ReactivityType.ClearWhen)
                            .WithReactivity(m => m.Shower == true, ReactivityType.VisibleWhen)
                        .End()
                    .End();
            moduleBuilder.AddFlowToModule("", "lab-calculator", "test", flow.Flow);
        }
    }
}
