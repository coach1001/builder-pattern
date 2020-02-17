using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Builders;
using CoreDui.Definitions;
using CoreDui.Enums;

namespace CoreDuiWebApi.Flow
{
    public static class TestFlow1
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder.WithFlow<SimpleFlowModel, ContextModel>("test-flow-1")
                    .WithStep(m => m.Step1)
                        .AddControl(m => m.Password)
                            .SetControlType(ControlType.Text)
                        .End()
                        .AddControl(m => m.ConfirmPassword)
                            .SetControlType(ControlType.Text)
                        .End()
                        .AddControl(m => m.Age)
                            .SetControlType(ControlType.Number)
                        .End()
                        .AddControl(m => m.HasDiabetes)
                            .SetControlType(ControlType.Boolean)                                
                        .End()
                        .AddControl(m => m.Skills)
                        .End()
                        .AddArray(m => m.Kids)
                            .AddControl(m => m.Name)
                            .End()
                        .End()
                    .End();
            moduleBuilder.AddFlowToModule("", "portal", "test-module", flow.Flow);
        }
    }
}
