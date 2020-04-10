using System;
using System.Collections.Generic;
using System.Text;

namespace CoreDui.Definitions
{
    public abstract class BaseContextModel
    {
        public Guid FlowId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
