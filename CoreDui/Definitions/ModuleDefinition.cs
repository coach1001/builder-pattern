using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDui.Definitions
{
    public class ModuleDefinition
    {
        public string Route { get; set; }
        public string System { get; set; }
        public string ModuleName { get; set; }
        public ICollection<FlowDefinition> Flows { get; set; } = new List<FlowDefinition>();
    }
}
