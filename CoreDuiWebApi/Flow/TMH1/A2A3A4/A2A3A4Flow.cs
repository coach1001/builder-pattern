using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Builders;
using CoreDui.Enums;

namespace CoreDuiWebApi.Flow.TMH1.A2A3A4
{
    public class A2A3A4Flow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<A2A3A4Model, A2A3A4Context>("a2a3a4")
                    .WithStep(m => m.Data)
                        .GridConfig("1fr 5fr")
                        .WithBorder(BorderEnum.ltrb)
                        .Next("Save")
                        .WithTask<A2A3A4CalculationTask>(TaskTypeEnum.PeriTask)
                        .AddDecorator("A2 - Liquid Limit")
                            .PositionConfig("1/3", "2")
                            .WithMetadata("textAlign", "center")
                            .WithMetadata("textWeight", "bold")
                        .End()
                        .AddArray(m => m.LiquidLimitPoints)
                            .PositionConfig("1/3", "2")
                            .GridConfig("")
                        .End()
                        .AddControl(m => m.AverageLiquidLimit, ControlType.Number, "Average Liquid Limit")
                            .PositionConfig("1", "3")
                        .End()
                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
