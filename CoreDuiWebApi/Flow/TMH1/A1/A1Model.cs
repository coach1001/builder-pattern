using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Attributes;

namespace CoreDuiWebApi.Flow.TMH1.A1
{
    public class A1Model
    {
        public A1DataStep Data;
    }

    public class A1DataStep
    {
        [Range(1000, 10000)]
        public decimal? TotalSampleMass { get; set; }        
        public decimal? TotalRetainedOn19mm { get; set; }
        [Range(1000, 10000)]
        public decimal? RiffledDryMass { get; set; }
        public decimal? ReductionFactor { get; set; }
        public decimal? InitialSieve { get; set; }
        public decimal? Washed { get; set; }
        public decimal? Final { get; set; }

        [MustBeLessThan(nameof(TotalSampleMass))]
        public decimal? MassRetained105mm { get; set; }
        public decimal? PercRetained105mm { get; set; }
        public decimal? PercPassing105mm { get; set; }

        [MustBeLessThan(nameof(MassRetained105mm))]
        public decimal? MassRetained75mm { get; set; }
        public decimal? PercRetained75mm { get; set; }
        public decimal? PercPassing75mm { get; set; }

        public decimal? MassRetained63mm { get; set; }
        public decimal? PercRetained63mm { get; set; }
        public decimal? PercPassing63mm { get; set; }

        public decimal? MassRetained53mm { get; set; }
        public decimal? PercRetained53mm { get; set; }
        public decimal? PercPassing53mm { get; set; }

        public decimal? MassRetained37_5mm { get; set; }
        public decimal? PercRetained37_5mm { get; set; }
        public decimal? PercPassing37_5mm { get; set; }

        public decimal? MassRetained26_5mm { get; set; }
        public decimal? PercRetained26_5mm { get; set; }
        public decimal? PercPassing26_5mm { get; set; }

        public decimal? MassRetained19mm { get; set; }
        public decimal? PercRetained19mm { get; set; }
        public decimal? PercPassing19mm { get; set; }

        public decimal? MassRetained13_2mm { get; set; }
        public decimal? PercRetained13_2mm { get; set; }
        public decimal? PercPassing13_2mm { get; set; }

        public decimal? MassRetained4_75mm { get; set; }
        public decimal? PercRetained4_75mm { get; set; }
        public decimal? PercPassing4_75mm { get; set; }

        public decimal? MassRetained2mm { get; set; }
        public decimal? PercRetained2mm { get; set; }
        public decimal? PercPassing2mm { get; set; }

        public decimal? MassRetained0_425mm { get; set; }
        public decimal? PercRetained0_425mm { get; set; }
        public decimal? PercPassing0_425mm { get; set; }

        public decimal? MassRetainedLess0_425mm { get; set; }
        public decimal? PercRetainedLess10_425mm { get; set; }

        public decimal? GradingModulus { get; set; }
    }
}
