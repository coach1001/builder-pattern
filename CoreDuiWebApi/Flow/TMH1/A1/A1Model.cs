using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDuiWebApi.Flow.TMH1.A1
{
    public class A1Model
    {
        public A1DataStep Data;
    }

    public class A1DataStep
    {
        [Required]
        public float TotalSampleMass { get; set; }
        [Required]
        public float TotalRetainedOn19mm { get; set; }
        [Required]
        public float RiffledDryMass { get; set; }
        [Required]
        public float ReductionFactor { get; set; }
        public float InitialSieve { get; set; }
        public float Washed { get; set; }
        public float Final { get; set; }

        public float MassRetained105mm { get; set; }
        public float PercRetained105mm { get; set; }
        public float PercPassing105mm { get; set; }

        public float MassRetained75mm { get; set; }
        public float PercRetained75mm { get; set; }
        public float PercPassing75mm { get; set; }

        public float MassRetained63mm { get; set; }
        public float PercRetained63mm { get; set; }
        public float PercPassing63mm { get; set; }

        public float MassRetained53mm { get; set; }
        public float PercRetained53mm { get; set; }
        public float PercPassing53mm { get; set; }

        public float MassRetained37_5mm { get; set; }
        public float PercRetained37_5mm { get; set; }
        public float PercPassing37_5mm { get; set; }

        public float MassRetained26_5mm { get; set; }
        public float PercRetained26_5mm { get; set; }
        public float PercPassing26_5mm { get; set; }

        public float MassRetained19mm { get; set; }
        public float PercRetained19mm { get; set; }
        public float PercPassing19mm { get; set; }

        public float MassRetained13_2mm { get; set; }
        public float PercRetained13_2mm { get; set; }
        public float PercPassing13_2mm { get; set; }

        public float MassRetained4_75mm { get; set; }
        public float PercRetained4_75mm { get; set; }
        public float PercPassing4_75mm { get; set; }

        public float MassRetained2mm { get; set; }
        public float PercRetained2mm { get; set; }
        public float PercPassing2mm { get; set; }

        public float MassRetained0_425mm { get; set; }
        public float PercRetained0_425mm { get; set; }
        public float PercPassing0_425mm { get; set; }

        public float MassRetainedLess0_425mm { get; set; }
        public float PercRetainedLess10_425mm { get; set; }

        public float GradingModulus { get; set; }
    }
}
