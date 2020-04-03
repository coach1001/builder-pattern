using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Attributes;

namespace CoreDuiWebApi.Flow.TMH1.A2A3A4
{
    public class A2A3A4Model
    {
        public A2A3A4DataStep Data;
    }

    public class A2A3A4DataStep
    {
        [Range(1,3)]
        public ICollection<LiquidLimitPoint> LiquidLimitPoints { get; set; }
        public decimal AverageLiquidLimit { get; set; }
        [Range(1, 3)]
        public ICollection<PlasticLimitPoint> PlasticLimitPoints { get; set; }
        public decimal AveragePlasticLimit { get; set; }
        public decimal PlasticityIndex { get; set; }
    }

    public class LiquidLimitPoint
    {
        public string PanNumber { get; set; }
        public int Blows { get; set; }
        public decimal WetMass { get; set; }
        public decimal DryMass { get; set; }
        public decimal PanMass { get; set; }
        public decimal LiquidLimit { get; set; }        
    }

    public class PlasticLimitPoint
    {
        public string PanNumber { get; set; }        
        public decimal WetMass { get; set; }
        public decimal DryMass { get; set; }
        public decimal PanMass { get; set; }
        public decimal PlasticLimit { get; set; }

    }
}
