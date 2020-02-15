using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDui.Definitions
{
    public class SelectOption
    {
        public string Key { get; set; }
        public string Display { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}
