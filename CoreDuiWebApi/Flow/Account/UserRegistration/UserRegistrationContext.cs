
using System;
using CoreDui.Definitions;

namespace CoreDuiWebApi.Flow.Account.UserRegistration
{
    public class UserRegistrationContext : BaseContextModel
    {
        public string RegistrationEmailAddress { get; set; }
        public bool UserWithEmailAddressAlreadyExists { get; set; } = false;
        public bool VerificationEmailSent { get; set; } = false;
    }
}
