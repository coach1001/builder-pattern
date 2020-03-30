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

        public float MassRetained13_2mm { get; set; }
        public float PercRetained13_2mm { get; set; }
        public float PercPassing13_2mm { get; set; }

    }
}
