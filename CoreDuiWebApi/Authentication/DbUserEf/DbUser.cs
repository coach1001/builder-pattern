using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDuiWebApi.Authentication.DbUserEf
{
    public class DbUser : Common
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }                
        public string ProviderId { get; set; }        
        public string EmailAddress { get; set;}
        [Required]
        public string ProviderType { get; set; }
        public string Name { get; private set; }
        public string DisplayName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public bool AccountEnabled { get; set; }
        public Guid ConfirmEmailToken { get; set; }
        public DateTime ConfirmEmailTokenExpiresAt { get; set; }
        public Guid RefreshToken { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public Guid ResetPasswordToken { get; set; }
        public DateTime ResetPasswordTokenExpiresAt { get; set; }

    }
}
