using System.Collections.Generic;

namespace CoreDui.Definitions
{
    public class ValidatorData
    {        
        public string Name { get; set; }
        public bool ParentScope { get; set; } = false;
        public Dictionary<string, object> Metadata { get; set; }
    }
}
