using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CoreDuiWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreDuiWebApi.Authentication.Data
{
    [Table("DbUserRoles")]
    public class DbUserRole : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        public virtual string Role { get; set; }

        public virtual Guid DbUserId { get; set; }
    }
    
}
