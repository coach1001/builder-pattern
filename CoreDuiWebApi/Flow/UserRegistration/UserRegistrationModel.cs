
namespace CoreDuiWebApi.Flow.UserRegistration
{
    public class UserRegistrationModel
    {        
        public UserRegistrationDetails UserRegistrationDetails { get; set; }
    }

    public class UserRegistrationDetails
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
