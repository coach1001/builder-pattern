using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Builders;
using CoreDui.Enums;

namespace CoreDuiWebApi.Flow.TMH1.TestFlow
{
    public static class TestFlow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<TestData, TestContext>("test-flow")
                    .WithStep(m => m.TestStep)
                        .Next("Next")
                        .ConfigTracks(GridMediaSize.Large, "1fr 3fr")                        
                        .AddControl(m => m.Title).End()
                        .AddGroup(m => m.NestedObject)
                            .ConfigTracks(GridMediaSize.Large, "1fr 3fr")                            
                            .AddControl(m => m.NestedObjectProp).End()
                            .AddGroup(m => m.DeepNestedObject)
                                .AddControl(m => m.DeepNestedObjectProp).End()
                            .End()
                        .End()
                        .AddArray(m => m.NestedArray)
                            .ConfigTracks(GridMediaSize.Large, "1fr 1fr")
                            .ConfigSpans(GridMediaSize.Large, 2, 1)
                            .AddControl(m => m.NestedArrayObjectProp1).End()
                            .AddControl(m => m.NestedArrayObjectProp2).End()
                        .End()
                    .End();
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
