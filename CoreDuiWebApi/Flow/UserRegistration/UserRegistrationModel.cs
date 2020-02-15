
using System.ComponentModel.DataAnnotations;
using CoreDui.Attributes;

namespace CoreDuiWebApi.Flow.UserRegistration
{
    public class UserRegistrationModel
    {        
        public UserRegistrationDetails UserRegistrationDetails { get; set; }
        public object RegistrationDone { get; set; }
    }

    public class UserRegistrationDetails
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [MinLength(3)]
        public string Password { get; set; }
        [Required]
        [MustMatch(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
