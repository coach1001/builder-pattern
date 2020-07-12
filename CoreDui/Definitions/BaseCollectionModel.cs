using System;
using System.Collections.Generic;
using System.Text;
using CoreDui.Enums;
using Newtonsoft.Json;

namespace CoreDui.Definitions
{
    public abstract class BaseCollectionModel
    {        
        public Guid? Id__ { get; set; } 
        public ArrayOperation? Operation__ { get; set; }
    }
}
