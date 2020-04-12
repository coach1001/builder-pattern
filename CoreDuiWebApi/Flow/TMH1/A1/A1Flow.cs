using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Builders;
using CoreDui.Enums;
using CoreDuiWebApi.Flow.Constants;
using CoreDuiWebApi.Flow.TMH1.A1.FlowConstants;

namespace CoreDuiWebApi.Flow.TMH1.A1
{
    public class A1Flow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<A1Model, A1Context>("a1")
                    .RequiresAuthorization()
                    .WithStep(m => m.Data)
                        .GridConfig("3fr 6fr")                        
                        .WithBorder(BorderEnum.ltrb)
                        .Next("Save")
                        .WithTask<A1CalculationTask>(TaskTypeEnum.PeriTask)
                        .AddDecorator("A1 - Grading")
                            .PositionConfig("1/3", "1")
                            .WithMetadata("textAlign", "center")
                            .WithMetadata("textWeight", "bold")
                        .End()
                        .AddDecorator("Initial Sieve").PositionConfig("1", "3").End()
                        .AddControl(m => m.TotalSampleMass, ControlType.Number, "Total Sample Mass").WithSuffix(Appendixes.Grams).PositionConfig("1", "4").End()
                        .AddControl(m => m.TotalRetainedOn19mm, ControlType.Number, "Total Retained 19mm Sieve").WithSuffix(Appendixes.Grams).InitiallyDisabled().PositionConfig("1", "5").End()
                        .AddControl(m => m.RiffledDryMass, ControlType.Number, "Riffled Dry Mass ( < 19 mm)").WithSuffix(Appendixes.Grams).PositionConfig("1", "6").End()
                        .AddControl(m => m.ReductionFactor, ControlType.Number, "Reduction factor").WithSuffix("factor").InitiallyDisabled().PositionConfig("1", "7").End()
                        .AddControl(m => m.InitialSieve, ControlType.Number, "Initial Sieve (<0.425 mm)").WithSuffix(Appendixes.Grams).PositionConfig("1", "8").End()
                        .AddControl(m => m.Washed, ControlType.Number, "Washed (<0.425 mm)").WithSuffix(Appendixes.Grams).PositionConfig("1", "9").End()
                        .AddControl(m => m.Final, ControlType.Number, "Final (<0.425 mm)").WithSuffix(Appendixes.Grams).PositionConfig("1", "10").End()                        
                        .AddDecorator("Grading").WithMetadata("textWeight", "bold").PositionConfig("1/3", "11").End()                        
                        .AddArray(m => m.Sieves)
                            .PositionConfig("1/3", "12")
                            .GridConfig("1fr 1fr 1fr 1fr") 
                            .HideAddAndDelete()
                            .WithDefaultValue(SievesConstant.A1Sieves)                            
                            .AddControl(m => m.Size, ControlType.Number, "Sieve Size").PositionConfig("1").WithSuffix(Appendixes.Millimeters).InitiallyDisabled().End()
                            .AddControl(m => m.MassRetained, ControlType.Number, "Mass Retained").WithSuffix(Appendixes.Grams).PositionConfig("2").End()
                            .AddControl(m => m.PercPassing, ControlType.Number, "Percentage Passing").WithSuffix(Appendixes.Percentage).PositionConfig("3").InitiallyDisabled().End()
                            .AddControl(m => m.PercRetained, ControlType.Number, "Percentage Retained").WithSuffix(Appendixes.Percentage).PositionConfig("4")
                                .WithReactivity(m => m.Size != "< 0.425", ReactivityType.VisibleWhen)
                                .InitiallyDisabled()
                            .End()
                        .End()
                        .AddControl(m => m.GradingModulus, ControlType.Number, "Grading Modulus").InitiallyDisabled().PositionConfig("1", "13").End()
                        
                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
