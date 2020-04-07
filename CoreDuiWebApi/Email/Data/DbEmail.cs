using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreDuiWebApi.Data;
using CoreDuiWebApi.Email;

namespace CoreDuiWebApi.Email.Data
{
    public class DbEmail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        public virtual string Message { get; set; }
        public virtual string Status { get; set; } = "QUEUED";
        public virtual int RetryCount { get; set; } = 0;
    }
}
