using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreDuiWebApi.Email;

namespace CoreDuiWebApi.Email.DbEmailEf
{
    public class DbEmail : Common
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }                
        public string Message { get; set; }
        public string Status { get; set; } = "QUEUED";
        public int RetryCount { get; set; } = 0;
    }
}
