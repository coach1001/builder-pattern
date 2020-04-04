using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreDui.Attributes;

namespace CoreDuiWebApi.Flow.TMH1.A7
{
    public class A7Model
    {
        public A7DataStep Data;
    }

    public class A7DataStep
    {
        [Required]
        [CollectionRange(1,5)]
        public ICollection<DensityPoint> DensityPoints { get; set; }
        public decimal? MaximumDryDensity { get; set; }
        public decimal? OptimumMoistureContent { get; set; }

    }

    public class DensityPoint
    {
        public decimal? SampleMass { get; set; }
        public decimal? WaterPercAdded { get; set; }
        public decimal? WaterAdded { get; set; }
        public string MouldNumber { get; set; }
        public decimal? MouldMass { get; set; }
        public decimal? MouldVolume { get; set; }
        public decimal? MouldPlusWetMass { get; set; }
        public decimal? WetDensity { get; set; }
        public decimal? EstimatedDryDensity { get; set; }
        public string PanNumber { get; set; }
        public decimal? WetMassPlusPan { get; set; }
        public decimal? DryMassPlusPan { get; set; }
        public decimal? PanMass { get; set; }
        public decimal? ActualMoistureContent { get; set; }
        public decimal? ActualDryDensity { get; set; }
        public decimal? HygroscopicMoisture { get; set; }
    }
}
