using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Builders;
using CoreDui.Enums;
using CoreDuiWebApi.Flow.Constants;

namespace CoreDuiWebApi.Flow.TMH1.A7
{
    public class A7Flow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<A7Model, A7Context>("a7")                    
                    .WithStep(m => m.Data)
                        .GridConfig("repeat(5,1fr)")
                        .WithBorder(BorderEnum.ltrb)
                        .Next("Save")
                        .WithTask<A7CalculationTask>(TaskTypeEnum.PeriTask)
                        .AddDecorator("A2 - The determination of the maximum dry density and optimum moisture content of gravel, soil and sand")
                            .PositionConfig("2/5", "1")
                            .WithMetadata("textAlign", "center")
                            .WithMetadata("textWeight", "bold")
                        .End()                
                        .AddArray(m => m.DensityPoints, "Samples")
                            .PositionConfig("1/6", "2")
                            .GridConfig("1fr")
                            .Vertical(5)
                            .MaxRows(5)
                            .AddControl(m => m.SampleMass, ControlType.Text, "Sample Mass").End()
                            .AddControl(m => m.WaterPercAdded, ControlType.Number, "Water % Added").WithSuffix(Appendixes.Percentage).End()
                            .AddControl(m => m.WaterAdded, ControlType.Number, "Water Added").InitiallyDisabled().WithSuffix(Appendixes.MilliLiter).End()
                            .AddControl(m => m.MouldNumber, ControlType.Text, "Mould Number").End()
                            .AddControl(m => m.MouldMass, ControlType.Number, "MouldMass").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.MouldVolume, ControlType.Number, "MouldVolume").WithSuffix(Appendixes.MilliLiterCubed, true).End()
                            .AddControl(m => m.MouldPlusWetMass, ControlType.Number, "Mould + Wet Mass").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.WetDensity, ControlType.Number, "Wet Density").InitiallyDisabled().WithSuffix(Appendixes.KilogramMeterCubed, true).End()
                            .AddControl(m => m.EstimatedDryDensity, ControlType.Number, "Estimated Dry Density").InitiallyDisabled().WithSuffix(Appendixes.KilogramMeterCubed, true).End()
                            .AddControl(m => m.PanNumber, ControlType.Text, "Pan Number").End()
                            .AddControl(m => m.WetMassPlusPan, ControlType.Number, "Wet mass + Pan").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.DryMassPlusPan, ControlType.Number, "Dry mass + Pan").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.PanMass, ControlType.Number, "Pan Mass").WithSuffix(Appendixes.Grams).End()
                            .AddControl(m => m.ActualMoistureContent, ControlType.Number, "Actual Moisture Content").InitiallyDisabled().WithSuffix(Appendixes.Percentage).End()
                            .AddControl(m => m.ActualDryDensity, ControlType.Number, "Actual Dry Density").InitiallyDisabled().WithSuffix(Appendixes.KilogramMeterCubed, true).End()
                            .AddControl(m => m.HygroscopicMoisture, ControlType.Number, "Hygroscopic Moisture").InitiallyDisabled().WithSuffix(Appendixes.Percentage).End()
                        .End()
                        .AddControl(m => m.MaximumDryDensity, ControlType.Number, "Maximum Dry Density").PositionConfig("1", "3").InitiallyDisabled().WithSuffix(Appendixes.KilogramMeterCubed, true).End()
                        .AddControl(m => m.OptimumMoistureContent, ControlType.Number, "Optimum Moisture Content").PositionConfig("1", "4").InitiallyDisabled().WithSuffix(Appendixes.Percentage).End()

                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
