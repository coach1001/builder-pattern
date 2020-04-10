using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Builders;
using CoreDui.Enums;
using CoreDuiWebApi.Flow.Constants;

namespace CoreDuiWebApi.Flow.TMH1.A2A3A4
{
    public class A2A3A4Flow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<A2A3A4Model, A2A3A4Context>("a2a3a4")                    
                    .WithStep(m => m.Data)
                        .GridConfig("1fr 1fr 1fr")
                        .WithBorder(BorderEnum.ltrb)
                        .Next("Save")
                        .WithTask<A2A3A4CalculationTask>(TaskTypeEnum.PeriTask)
                        .AddDecorator("A2 - Liquid Limit")
                            .PositionConfig("1/4", "1")
                            .WithMetadata("textAlign", "center")
                            .WithMetadata("textWeight", "bold")
                        .End()                        
                        .AddArray(m => m.LiquidLimitPoints, "Liquid limit points")
                            .Vertical(3)
                            .MaxRows(3)
                            .GridConfig("1fr")
                            .PositionConfig("1/4", "2")
                            .AddControl(m => m.PanNumber, ControlType.Text, "Pan Number").End()
                            .AddControl(m => m.Blows, ControlType.Number, "Blows").End()
                            .AddControl(m => m.WetMass, ControlType.Number, "Wet Mass").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.DryMass, ControlType.Number, "Dry Mass").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.PanMass, ControlType.Number, "Pan Mass").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.LiquidLimit, ControlType.Number, "Liquid Limit").WithSuffix(Appendixes.Percentage).InitiallyDisabled().End()
                        .End()
                        .AddControl(m => m.AverageLiquidLimit, ControlType.Number, "Average Liquid Limit")
                            .InitiallyDisabled()
                            .WithSuffix("%")
                            .PositionConfig("1", "3")
                        .End()
                        .AddDecorator("A3 - Plastic Limit")
                            .PositionConfig("1/4", "4")
                            .WithMetadata("textAlign", "center")
                            .WithMetadata("textWeight", "bold")
                        .End()
                        .AddArray(m => m.PlasticLimitPoints, "Plastic limit points")
                            .Vertical(3)
                            .MaxRows(3)
                            .GridConfig("1fr")
                            .PositionConfig("1/4", "5")
                            .AddControl(m => m.PanNumber, ControlType.Text, "Pan Number").End()                            
                            .AddControl(m => m.WetMass, ControlType.Number, "Wet Mass").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.DryMass, ControlType.Number, "Dry Mass").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.PanMass, ControlType.Number, "Pan Mass").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.PlasticLimit, ControlType.Number, "Liquid Limit").WithSuffix(Appendixes.Percentage).InitiallyDisabled().End()
                        .End()
                        .AddControl(m => m.AveragePlasticLimit, ControlType.Number, "Average Liquid Limit")
                            .InitiallyDisabled()
                            .WithSuffix(Appendixes.Percentage)
                            .PositionConfig("1", "6")
                        .End()
                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
