
using System.ComponentModel.DataAnnotations;
using CoreDui.Attributes;
using CoreDui.Definitions;

namespace CoreDuiWebApi.Flow.Account.UserLogin
{
    public class UserLoginModel
    {        
        public UserLoginDetails UserLoginDetails { get; set; }        
    }

    public class UserLoginDetails
    {
        [Required]        
        public string UsernameOrEmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
