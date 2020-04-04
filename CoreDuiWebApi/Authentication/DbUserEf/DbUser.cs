using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreDuiWebApi.Authentication.DbUserRoleEf;

namespace CoreDuiWebApi.Authentication.DbUserEf
{
    public class DbUser : Common
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        public virtual string ProviderId { get; set; }
        public virtual string EmailAddress { get; set;}
        [Required]
        public virtual string ProviderType { get; set; }
        public virtual string Name { get; private set; }
        public virtual string DisplayName { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        [Required]
        public virtual bool AccountEnabled { get; set; }
        public virtual Guid ConfirmEmailToken { get; set; }
        public virtual DateTime ConfirmEmailTokenExpiresAt { get; set; }
        public virtual Guid RefreshToken { get; set; }
        public virtual DateTime RefreshTokenExpiresAt { get; set; }
        public virtual string Hash { get; set; }
        public virtual string Salt { get; set; }
        public virtual Guid ResetPasswordToken { get; set; }
        public virtual DateTime ResetPasswordTokenExpiresAt { get; set; }
        public virtual ICollection<DbUserRole> Roles { get; set; }
    }
}
