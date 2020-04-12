using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreDui.Attributes;
using CoreDui.Definitions;
using CoreDui.JsonSerializers.Collection;
using Newtonsoft.Json;

namespace CoreDuiWebApi.Flow.Test
{
    public class SimpleData
    {
        public SimpleDataStep1 Step1 { get; set; }
    }

    public class SimpleDataStep1
    {
        public bool? Shower { get; set; }

        [RequiredIf(nameof(Shower), true)]        
        public string DependsOnShower { get; set; }
    }

}
