using System;
using System.Collections.Generic;
using CoreDui.Enums;
using CoreDui.Repositories;
using CoreDui.TaskHandling;
using Newtonsoft.Json;

namespace CoreDui.Definitions
{
    public class Element
    {
        public string Name { get; set; }
        public string TaskPath { get; set; }
        public string ModelProperty { get; set; }
        public string UiTemplate { get; set; }
        public ElementType ElementType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<Element> Elements { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ControlType? ControlType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object> Metadata { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<ValidatorData> Validators { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<IFlowTask> Tasks { get; set; }

        [JsonIgnore]        
        public Type DataType { get; set; }
        
    }

}
