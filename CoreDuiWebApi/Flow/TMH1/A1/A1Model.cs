using System;
using System.Collections.Generic;
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
        public float TotalSampleMass { get; set; }
        public float TotalRetainedOn19mm { get; set; }
        public float RiffledDryMass { get; set; }
        public float ReductionFactor { get; set; }
        public float InitialSieve { get; set; }
        public float Washed { get; set; }
        public float Final { get; set; }
    }
}
