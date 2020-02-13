using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace CoreDui.Definitions
{
    public class ModuleDelegationType
    {     
        public string System { get; set; }
        public string Module { get; set; }
        public string Route { get; set; }
        public ModuleDefinition ModuleDefinition { get; set; }        
    }
}
