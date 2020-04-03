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
                        .GridConfig("minmax(0,5fr) 1fr repeat(3,minmax(0,5fr))")
                        .WithBorder(BorderEnum.ltrb)
                        .Next("Save")                        
                        .WithTask<A1CalculationTask>(TaskTypeEnum.PeriTask)                                                
                        .AddDecorator("A1 - Grading")
                            .PositionConfig("1/6", "1")
                            .WithMetadata("textAlign", "center")
                            .WithMetadata("textWeight", "bold")
                        .End()

                        .AddDecorator("Initial Sieve").PositionConfig("1", "3").End()
                        .AddControl(m => m.TotalSampleMass, ControlType.Number, "Total Sample Mass").WithSuffix("grams").PositionConfig("1", "4").End()                                                
                        .AddControl(m => m.TotalRetainedOn19mm, ControlType.Number, "Total Retained 19mm Sieve").WithSuffix("grams").InitiallyDisabled().PositionConfig("1", "5").End()                                                
                        .AddControl(m => m.RiffledDryMass, ControlType.Number, "Riffled Dry Mass ( < 19 mm)").WithSuffix("grams").PositionConfig("1", "6").End()                                                
                        .AddControl(m => m.ReductionFactor, ControlType.Number, "Reduction factor").WithSuffix("factor").InitiallyDisabled().PositionConfig("1", "7").End()                        
                        .AddControl(m => m.InitialSieve, ControlType.Number, "Initial Sieve (<0.425 mm)").WithSuffix("grams").PositionConfig("1", "8").End()                                                
                        .AddControl(m => m.Washed, ControlType.Number, "Washed (<0.425 mm)").WithSuffix("grams").PositionConfig("1", "9").End()                                                
                        .AddControl(m => m.Final, ControlType.Number, "Final (<0.425 mm)").WithSuffix("grams").PositionConfig("1", "10").End()

                        .AddDecorator("Grading").WithMetadata("textWeight", "bold").PositionConfig("3/6", "2").End()                      
                        .AddDecorator("Mass Retained").WithMetadata("textWeight", "bold").PositionConfig("3", "3").End()
                        .AddDecorator("Percentage Retained").WithMetadata("textWeight", "bold").PositionConfig("4", "3").End()
                        .AddDecorator("Percentage Passing").WithMetadata("textWeight", "bold").PositionConfig("5", "3").End()

                        .AddControl(m => m.MassRetained105mm, ControlType.Number,"105 mm").WithSuffix("grams").PositionConfig("3", "4").End()
                        .AddControl(m => m.PercRetained105mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "4").End()
                        .AddControl(m => m.PercPassing105mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("5", "4").End()

                        .AddControl(m => m.MassRetained75mm, ControlType.Number, "75 mm").WithSuffix("grams").PositionConfig("3", "5").End()
                        .AddControl(m => m.PercRetained75mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "5").End()
                        .AddControl(m => m.PercPassing75mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("5", "5").End()                        
                        
                        .AddControl(m => m.MassRetained63mm, ControlType.Number, "63 mm").WithSuffix("grams").PositionConfig("3", "6").End()
                        .AddControl(m => m.PercRetained63mm, ControlType.Number, "").WithSuffix("%").PositionConfig("4", "6").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing63mm, ControlType.Number, "").WithSuffix("%").PositionConfig("5", "6").InitiallyDisabled().End()                        
                        
                        .AddControl(m => m.MassRetained53mm, ControlType.Number, "53 mm").WithSuffix("grams").PositionConfig("3", "7").End()
                        .AddControl(m => m.PercRetained53mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "7").End()
                        .AddControl(m => m.PercPassing53mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("5", "7").End()                        
                        
                        .AddControl(m => m.MassRetained37_5mm, ControlType.Number, "37.5 mm").WithSuffix("grams").PositionConfig("3", "8").End()
                        .AddControl(m => m.PercRetained37_5mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "8").End()
                        .AddControl(m => m.PercPassing37_5mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("5", "8").End()                        
                        
                        .AddControl(m => m.MassRetained26_5mm, ControlType.Number, "26.5 mm").WithSuffix("grams").PositionConfig("3", "9").End()
                        .AddControl(m => m.PercRetained26_5mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "9").End()
                        .AddControl(m => m.PercPassing26_5mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("5", "9").End()                        
                        
                        .AddControl(m => m.MassRetained19mm, ControlType.Number, "19 mm").WithSuffix("grams").PositionConfig("3", "10").End()
                        .AddControl(m => m.PercRetained19mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "10").End()                        
                        .AddControl(m => m.PercPassing19mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("5", "10").End()                        
                        
                        .AddControl(m => m.MassRetained13_2mm, ControlType.Number, "13.2 mm").WithSuffix("grams").PositionConfig("3", "11").End()
                        .AddControl(m => m.PercRetained13_2mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "11").End()
                        .AddControl(m => m.PercPassing13_2mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("5", "11").End()                        
                        
                        .AddControl(m => m.MassRetained4_75mm, ControlType.Number, "4.75 mm").WithSuffix("grams").PositionConfig("3", "12").End()
                        .AddControl(m => m.PercRetained4_75mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "12").End()
                        .AddControl(m => m.PercPassing4_75mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("5", "12").End()                        
                        
                        .AddControl(m => m.MassRetained2mm, ControlType.Number, "2 mm").WithSuffix("grams").PositionConfig("3", "13").End()
                        .AddControl(m => m.PercRetained2mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "13").End()
                        .AddControl(m => m.PercPassing2mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("5", "13").End()                        
                        
                        .AddControl(m => m.MassRetained0_425mm, ControlType.Number, "0.425 mm").WithSuffix("grams").PositionConfig("3", "14").End()
                        .AddControl(m => m.PercRetained0_425mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "14").End()
                        .AddControl(m => m.PercPassing0_425mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("5", "14").End()                        
                                             
                        .AddControl(m => m.MassRetainedLess0_425mm, ControlType.Number, "< 0.425 mm").WithSuffix("grams").PositionConfig("3", "15").End()
                        .AddControl(m => m.PercRetainedLess10_425mm, ControlType.Number, "").WithSuffix("%").InitiallyDisabled().PositionConfig("4", "15").End()                                                
                        
                        .AddControl(m => m.GradingModulus, ControlType.Number, "Grading Modulus").InitiallyDisabled().PositionConfig("1", "12").End()
                        
                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
