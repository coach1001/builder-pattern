﻿using System;
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
                        .Next("Save")
                        
                        .ConfigTracks(GridMediaSize.Large, "1fr 1fr 1fr 1fr 1fr")                                                

                        .AddDecorator("TMH1 - A1")
                            .ConfigSpans(GridMediaSize.Large, 5)
                            .WithMetadata("textAlign", "center")
                            .WithMetadata("textWeight", "bold")
                            .WithBorder(BorderEnum.ltrb)
                        .End()

                        .AddDecorator("Total Sample Mass").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.TotalSampleMass, ControlType.Number).WithSuffix("grams").WithBorder(BorderEnum.rb).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Total Retained 19mm Sieve").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()                        
                        .AddControl(m => m.TotalRetainedOn19mm, ControlType.Number).WithSuffix("grams").WithBorder(BorderEnum.rb).InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Riffled Dry Mass ( < 19 mm)").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.RiffledDryMass, ControlType.Number).WithSuffix("grams").WithBorder(BorderEnum.rb).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Reduction factor").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.ReductionFactor, ControlType.Number).WithSuffix("factor").WithBorder(BorderEnum.rb).InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Initial Sieve (<0.425 mm)").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.InitialSieve, ControlType.Number).WithSuffix("grams").WithBorder(BorderEnum.rb).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Washed (<0.425 mm)").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.Washed, ControlType.Number).WithSuffix("grams").WithBorder(BorderEnum.rb).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Final (<0.425 mm)").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.Final, ControlType.Number).WithSuffix("grams").WithBorder(BorderEnum.rb).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                        .AddDecorator("Sieve Arpeture Size").WithBorder(BorderEnum.lrb).End()
                        .AddDecorator("Mass Retained").WithBorder(BorderEnum.rb).End()
                        .AddDecorator("Percentage Retained").WithBorder(BorderEnum.trb).End()
                        .AddDecorator("Percentage Passing").WithBorder(BorderEnum.trb).End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("105 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetained105mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained105mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing105mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("75 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetained75mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained75mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing75mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("63 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetained63mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained63mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing63mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("53 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetained53mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained53mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing53mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("37.5 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetained37_5mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained37_5mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing37_5mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("26.5 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetained26_5mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained26_5mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing26_5mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("19 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetained19mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained19mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing19mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("13.2 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltrb).End()
                        .AddControl(m => m.MassRetained13_2mm, ControlType.Number).WithBorder(BorderEnum.trb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained13_2mm, ControlType.Number).WithBorder(BorderEnum.trb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing13_2mm, ControlType.Number).WithBorder(BorderEnum.trb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("4.75 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetained4_75mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained4_75mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing4_75mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("2.0 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetained2mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained2mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing2mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("0.425 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetained0_425mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetained0_425mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing0_425mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 1).End()

                        .AddDecorator("< 0.425 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.lrb).End()
                        .AddControl(m => m.MassRetainedLess0_425mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("grams").End()
                        .AddControl(m => m.PercRetainedLess10_425mm, ControlType.Number).WithBorder(BorderEnum.rb).WithSuffix("%").InitiallyDisabled().End()                        
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 2).End()

                        .AddDecorator("Grading Modulus").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltrb).End()
                        .AddControl(m => m.GradingModulus, ControlType.Number).WithBorder(BorderEnum.trb).InitiallyDisabled().End()
                        .AddSpacer().ConfigSpans(GridMediaSize.Large, 3).End()

                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
