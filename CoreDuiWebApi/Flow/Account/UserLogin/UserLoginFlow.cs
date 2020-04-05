
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
                        .GridConfig("2fr 5fr 2fr")
                        .AddControl(m => m.UsernameOrEmailAddress, ControlType.Text, "Username or Email address")
                            .PositionConfig("2/3", "1")
                        .End()
                        .AddControl(m => m.Password, ControlType.HideableText, "Password")
                            .PositionConfig("2/3", "2")
                        .End()
                        .WithTask<UserLoginTask>(TaskTypeEnum.PostTask)
                        .WithUiTask(TaskTypeEnum.PostTask, "login")
                    .End();
            moduleBuilder.AddFlowToModule("", "lab-calculator", "account", flow.Flow);
        }
    }
}
