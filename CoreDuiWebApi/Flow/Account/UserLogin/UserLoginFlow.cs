
using System.Collections.Generic;
using CoreDui.Builders;
using CoreDui.Definitions;
using CoreDui.Enums;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.Account.UserLogin
{
    public static class UserLoginFlow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<UserLoginModel, UserLoginContext>("user-login")
                    .WithStep(m => m.UserLoginDetails, "details", "create")    
                        .Next("Login")
                        .AddControl(m => m.UsernameOrEmailAddress, "Username or Email address", ControlType.Text)
                        .End()
                        .AddControl(m => m.Password, "Password", ControlType.HideableText)
                        .End()
                        .WithTask<UserLoginTask>(TaskTypeEnum.PostTask)
                    .End()
                    .WithStep(m => m.LoginDone, "done", "done")
                        .Next("Done")
                    .End();                    
            moduleBuilder.AddFlowToModule("", "portal", "account", flow.Flow);
        }
    }
}
