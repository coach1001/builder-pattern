using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Builders;
using CoreDui.Enums;

namespace CoreDuiWebApi.Flow.TMH1.A1
{
    public class A1Flow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<A1Model, A1Context>("a1")
                    .WithStep(m => m.Data)
                        .ConfigTracks(GridMediaSize.Large, "repeat(5, 1fr)", "repeat(40)")   
                        .AddControl(m => m.TotalSampleMass)
                            .SetControlType(ControlType.Number).ConfigSpans(GridMediaSize.Large, 1, 1)
                        .End()
                        .AddControl(m => m.TotalRetainedOn19mm)
                            .SetControlType(ControlType.Number).ConfigSpans(GridMediaSize.Large, 1, 1)
                        .End()
                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
