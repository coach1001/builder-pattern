using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDui.Definitions
{
    public class FlowDelegationType
    {     
        public string System { get; set; }
        public string ModuleName { get; set; }
        public string Route { get; set; }
        public FlowDefinition Flow { get; set; }
    }
}
