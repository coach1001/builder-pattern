using System;
using System.Collections.Generic;
using CoreDui.Enums;
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
        public ICollection<TaskDefinition> Tasks { get; set; }
        
        [JsonIgnore]        
        public Type DataType { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<SelectOption> Options { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Icon { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BackButton { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string NextButton { get; set; } = null;

        public GridConfig GridConfig { get; set; } = new GridConfig();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AppendInput Prefix;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AppendInput Suffix;
        
        public bool InitiallyDisabled = false;
        
        public BorderEnum BorderConfig = BorderEnum.none;
    }

}
