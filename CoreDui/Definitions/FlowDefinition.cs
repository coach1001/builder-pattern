using System;
using System.Collections.Generic;
using System.Text;
using CoreDui.TaskHandling;
using Newtonsoft.Json;

namespace CoreDui.Definitions
{
    public class FlowDefinition
    {
        public string Flow { get; set; }    

        public string TaskPath { get; set; }
        
        [JsonIgnore]
        public Type DataType { get; set; }
        
        [JsonIgnore]
        public Type ContextType { get; set; }
        
        public ICollection<IFlowTask> Tasks { get; set; }

        public ICollection<Element> Steps;
    }
}
