﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace CoreDui.Definitions
{
    public class FlowDelegationType
    {     
        public string System { get; set; }
        public string Module { get; set; }
        public string Route { get; set; }
        public FlowDefinition FlowDefinition { get; set; }        
    }
}
