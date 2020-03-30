
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
                        .AddControl(m => m.UsernameOrEmailAddress, ControlType.Text, "Username or Email address")
                        .End()
                        .AddControl(m => m.Password, ControlType.HideableText, "Password")
                        .End()
                        .WithTask<UserLoginTask>(TaskTypeEnum.PostTask)
                    .End()
                    .WithStep(m => m.LoginDone, "done", "done")
                        .Next("Done")
                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "account", flow.Flow);
        }
    }
}
