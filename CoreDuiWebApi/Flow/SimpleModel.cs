using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreDui.Attributes;

namespace CoreDuiWebApi.Flow
{
    public class SimpleFlowModel
    {
        public SimpleModel Step1 { get; set; }
    }

    public class SimpleModel
    {
        [Required]        
        [MinLength(3)]
        public string Password { get; set; }
        
        [Required]
        [MustMatch(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        public int Age { get; set; }

        [RequiredIf(nameof(Age), 21)]
        public bool? HasDiabetes { get; set; }

        public ICollection<string> Skills { get; set; }
        public ICollection<Kid> Kids { get; set; }

        public DateTime CurrentDateTime { get; set; }
    }

    public class Kid
    {
        public string Name { get; set; }
    }

    public class ContextModel
    {
        public DateTime UpdatedAt { get; set; }
    }
}
