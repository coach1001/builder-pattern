using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDuiWebApi.Email
{
    public class SendEmailResult
    {
        [Required]
        public bool Sent { get; set; }
        [Required]
        public string Reason { get; set; }
    }
}
