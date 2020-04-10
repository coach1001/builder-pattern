using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Attributes;
using CoreDui.Definitions;
using CoreDui.JsonSerializers.Collection;
using Newtonsoft.Json;

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

        [CollectionRange(1, 14)]
        [JsonConverter(typeof(BaseCollectionConverter))]
        public ICollection<Sieve> Sieves { get; set; }
        public decimal? GradingModulus { get; set; }
    }

    public class Sieve : BaseCollectionModel
    {
        public string Size { get; set; }
        public decimal? MassRetained { get; set; }
        public decimal? PercRetained { get; set; }
        public decimal? PercPassing { get; set; }        
    }
}
