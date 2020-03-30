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
                        .ConfigTracks(GridMediaSize.Large, "2fr 1fr 1fr 1fr 1fr")
                        
                        .AddDecorator("TMH1 - A1")
                            .ConfigSpans(GridMediaSize.Large, 5)
                            .WithMetadata("textAlign", "center")
                            .WithMetadata("textWeight", "bold")
                        .End()

                        .AddDecorator("Total Sample Mass").WithMetadata("textAlign", "right").End()
                        .AddControl(m => m.TotalSampleMass, ControlType.Number).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Total Retained 19mm Sieve").WithMetadata("textAlign", "right").End()                        
                        .AddControl(m => m.TotalRetainedOn19mm, ControlType.Number).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Riffled Dry Mass ( < 19 mm)").WithMetadata("textAlign", "right").End()
                        .AddControl(m => m.RiffledDryMass, ControlType.Number).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Reduction factor").WithMetadata("textAlign", "right").End()
                        .AddControl(m => m.ReductionFactor, ControlType.Number).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Initial Sieve (<0.425 mm").WithMetadata("textAlign", "right").End()
                        .AddControl(m => m.InitialSieve, ControlType.Number).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Washed (<0.425 mm)").WithMetadata("textAlign", "right").End()
                        .AddControl(m => m.Washed, ControlType.Number).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Final (<0.425 mm)").WithMetadata("textAlign", "right").End()
                        .AddControl(m => m.Final, ControlType.Number).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Sieve Arpeture Size").End()
                        .AddDecorator("Mass Retained").End()
                        .AddDecorator("% Retained").End()
                        .AddDecorator("% Passing").End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("105mm").WithMetadata("textAlign", "right").End()
                        .AddControl(m => m.MassRetained105mm, ControlType.Number).End()
                        .AddControl(m => m.PercRetained105mm, ControlType.Number).End()
                        .AddControl(m => m.PercPassing105mm, ControlType.Number).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("75mm").WithMetadata("textAlign", "right").End()
                        .AddControl(m => m.MassRetained75mm, ControlType.Number).End()
                        .AddControl(m => m.PercRetained75mm, ControlType.Number).End()
                        .AddControl(m => m.PercPassing75mm, ControlType.Number).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()
                        
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 5).End()

                        .AddDecorator("13.2mm").WithMetadata("textAlign", "right").End()
                        .AddControl(m => m.MassRetained13_2mm, ControlType.Number).End()
                        .AddControl(m => m.PercRetained13_2mm, ControlType.Number).End()
                        .AddControl(m => m.PercPassing13_2mm, ControlType.Number).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
