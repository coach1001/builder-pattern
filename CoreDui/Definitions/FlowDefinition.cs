using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CoreDui.Definitions
{
    public class FlowDefinition
    {
        public string Flow { get; set; }
        
        public bool RequiresAuthorization { get; set; } = false;

        public string TaskPath { get; set; }

        [JsonIgnore]
        public Type DataType { get; set; }

        [JsonIgnore]
        public Type ContextType { get; set; }

        public ICollection<TaskDefinition> Tasks { get; set; }

        public ICollection<Element> Steps;
        
        public ICollection<string> AllowedRoles = new List<string>();
    }

}
