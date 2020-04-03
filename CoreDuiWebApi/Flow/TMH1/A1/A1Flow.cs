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
                        .GridConfig("minmax(0,4fr) minmax(0,1fr) minmax(0,3fr) repeat(3,minmax(0,4fr))")
                        .WithBorder(BorderEnum.ltrb)
                        .Next("Save")                        
                        .WithTask<A1CalculationTask>(TaskTypeEnum.PeriTask)                                                
                        .AddDecorator("TMH1 - A1")
                            .PositionConfig("1/7", "1")
                            .WithMetadata("textAlign", "center")
                            .WithMetadata("textWeight", "bold")
                        .End()

                        .AddDecorator("Initial Sieve").PositionConfig("1", "2").End()
                        .AddControl(m => m.TotalSampleMass, ControlType.Number, "Total Sample Mass").WithSuffix("grams").PositionConfig("1", "3").End()                                                
                        .AddControl(m => m.TotalRetainedOn19mm, ControlType.Number, "Total Retained 19mm Sieve").WithSuffix("grams").InitiallyDisabled().PositionConfig("1", "4").End()                                                
                        .AddControl(m => m.RiffledDryMass, ControlType.Number, "Riffled Dry Mass ( < 19 mm)").WithSuffix("grams").PositionConfig("1", "5").End()                                                
                        .AddControl(m => m.ReductionFactor, ControlType.Number, "Reduction factor").WithSuffix("factor").InitiallyDisabled().PositionConfig("1", "6").End()                        
                        .AddControl(m => m.InitialSieve, ControlType.Number, "Initial Sieve (<0.425 mm)").WithSuffix("grams").PositionConfig("1", "7").End()                                                
                        .AddControl(m => m.Washed, ControlType.Number, "Washed (<0.425 mm)").WithSuffix("grams").PositionConfig("1", "8").End()                                                
                        .AddControl(m => m.Final, ControlType.Number, "Final (<0.425 mm)").WithSuffix("grams").PositionConfig("1", "9").End()

                        .AddDecorator("Grading").PositionConfig("3/7", "2").End()
                        .AddDecorator("Sieve Arperture Size").PositionConfig("3", "3").End()
                        .AddDecorator("Mass Retained").PositionConfig("4", "3").End()
                        .AddDecorator("Percentage Retained").PositionConfig("5", "3").End()
                        .AddDecorator("Percentage Passing").PositionConfig("6", "3").End()

                        .AddDecorator("50 mm").WithMetadata("textAlign", "right").PositionConfig("3", "4").End()
                        .AddControl(m => m.MassRetained105mm, ControlType.Number).WithSuffix("grams").PositionConfig("4", "4").End()
                        .AddControl(m => m.PercRetained105mm, ControlType.Number).WithSuffix("%").InitiallyDisabled().PositionConfig("5", "4").End()
                        .AddControl(m => m.PercPassing105mm, ControlType.Number).WithSuffix("%").InitiallyDisabled().PositionConfig("6", "4").End()                        
                        
                        /*.AddControl(m => m.MassRetained75mm, ControlType.Number).WithPrefix("75 mm").WithSuffix("grams").PositionConfig("1", "11").End()
                        .AddControl(m => m.PercRetained75mm, ControlType.Number).WithSuffix("%").InitiallyDisabled().PositionConfig("2", "11").End()
                        .AddControl(m => m.PercPassing75mm, ControlType.Number).WithSuffix("%").InitiallyDisabled().PositionConfig("3", "11").End()                        

                        .AddDecorator("63 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltr).PositionConfig("1/2", "12").End()
                        .AddControl(m => m.MassRetained63mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("grams").PositionConfig("2/3", "12").End()
                        .AddControl(m => m.PercRetained63mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").PositionConfig("3/4", "12").InitiallyDisabled().End()
                        .AddControl(m => m.PercPassing63mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").PositionConfig("4/5", "12").InitiallyDisabled().End()                        

                        .AddDecorator("53 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltr).PositionConfig("1/2", "13").End()
                        .AddControl(m => m.MassRetained53mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("grams").PositionConfig("2/3", "13").End()
                        .AddControl(m => m.PercRetained53mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("3/4", "13").End()
                        .AddControl(m => m.PercPassing53mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("4/5", "13").End()                        

                        .AddDecorator("37.5 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltr).PositionConfig("1/2", "14").End()
                        .AddControl(m => m.MassRetained37_5mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("grams").PositionConfig("2/3", "14").End()
                        .AddControl(m => m.PercRetained37_5mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("3/4", "14").End()
                        .AddControl(m => m.PercPassing37_5mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("4/5", "14").End()                        

                        .AddDecorator("26.5 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltr).PositionConfig("1/2", "15").End()
                        .AddControl(m => m.MassRetained26_5mm, ControlType.Number).WithBorder(BorderEnum.ltr).WithSuffix("grams").PositionConfig("2/3", "15").End()
                        .AddControl(m => m.PercRetained26_5mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("3/4", "15").End()
                        .AddControl(m => m.PercPassing26_5mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("4/5", "15").End()                        

                        .AddDecorator("19 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltr).PositionConfig("1/2", "16").End()
                        .AddControl(m => m.MassRetained19mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("grams").PositionConfig("2/3", "16").End()
                        .AddControl(m => m.PercRetained19mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("3/4", "16").End()                        
                        .AddControl(m => m.PercPassing19mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("4/5", "16").End()                        

                        .AddDecorator("13.2 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltr).PositionConfig("1/2", "17").End()
                        .AddControl(m => m.MassRetained13_2mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("grams").PositionConfig("2/3", "17").End()
                        .AddControl(m => m.PercRetained13_2mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("3/4", "17").End()
                        .AddControl(m => m.PercPassing13_2mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("4/5", "17").End()                        

                        .AddDecorator("4.75 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltr).PositionConfig("1/2", "18").End()
                        .AddControl(m => m.MassRetained4_75mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("grams").PositionConfig("2/3", "18").End()
                        .AddControl(m => m.PercRetained4_75mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("3/4", "18").End()
                        .AddControl(m => m.PercPassing4_75mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("4/5", "18").End()                        

                        .AddDecorator("2.0 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltr).PositionConfig("1/2", "19").End()
                        .AddControl(m => m.MassRetained2mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("grams").PositionConfig("2/3", "19").End()
                        .AddControl(m => m.PercRetained2mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("3/4", "19").End()
                        .AddControl(m => m.PercPassing2mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("4/5", "19").End()                        

                        .AddDecorator("0.425 mm").WithMetadata("textAlign", "right").WithBorder(BorderEnum.ltr).PositionConfig("1/2", "20").End()
                        .AddControl(m => m.MassRetained0_425mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("grams").PositionConfig("2/3", "20").End()
                        .AddControl(m => m.PercRetained0_425mm, ControlType.Number).WithBorder(BorderEnum.tr).WithSuffix("%").InitiallyDisabled().PositionConfig("3/4", "20").End()
                        .AddControl(m => m.PercPassing0_425mm, ControlType.Number).WithBorder(BorderEnum.trb).WithSuffix("%").InitiallyDisabled().PositionConfig("4/5", "20").End()                        
                                             
                        .AddControl(m => m.MassRetainedLess0_425mm, ControlType.Number).WithPrefix("<0.425 mm").WithSuffix("grams").PositionConfig("1", "12").End()
                        .AddControl(m => m.PercRetainedLess10_425mm, ControlType.Number).WithSuffix("%").InitiallyDisabled().PositionConfig("2", "12").End()                                                
                        
                        .AddControl(m => m.GradingModulus, ControlType.Number, "Grading Modulus").InitiallyDisabled().PositionConfig("1", "13").End()*/
                        
                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
