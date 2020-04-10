using System;
using System.Collections.Generic;
using System.Text;
using CoreDui.Enums;
using Newtonsoft.Json;

namespace CoreDui.Definitions
{
    public class BaseCollectionModel
    {        
        public Guid? Id__ { get; set; } = Guid.NewGuid();

        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public ArrayItemOperation? Operation__ { get; set; }
    }
}
 