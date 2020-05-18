using System;
using CoreDui.Definitions;

namespace CoreDuiWebApi.Flow.Account.UserEdit
{
    public class UserEditContext : BaseContextModel
    {
        public Guid EditingUser { get; set; }
    }
}
