
using System;
using CoreDui.Definitions;
using CoreDuiWebApi.Authentication;

namespace CoreDuiWebApi.Flow.Account.UserLogin
{
    public class UserLoginContext : BaseContextModel
    {
        public bool LoginSuccessfull { get; set; } = false;        
        public LdapUser LdapUser { get; set; }
        public DbUserClient DbUser { get; set; }

    }
}
