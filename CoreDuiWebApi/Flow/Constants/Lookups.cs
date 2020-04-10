using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Definitions;

namespace CoreDuiWebApi.Flow.Constants
{
    public static class Lookups
    {
        public static readonly ICollection<SelectOption> Gender = new List<SelectOption>
        {
            new SelectOption { Key = "M", Display = "Male" },
            new SelectOption { Key = "F", Display = "Female" },
        };
    }
}
