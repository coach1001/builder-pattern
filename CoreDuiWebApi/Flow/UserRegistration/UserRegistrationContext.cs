
using System;
using CoreDui.Definitions;

namespace CoreDuiWebApi.Flow.UserRegistration
{
    public class UserRegistrationContext : BaseContextModel
    {
        public string EmailAddress { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
