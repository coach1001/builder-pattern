
using CoreDui.Builders;
using CoreDui.Enums;

namespace CoreDuiWebApi.Flow.UserRegistration
{
    public static class UserRegistrationFlow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<UserRegistrationModel, UserRegistrationContext>("user-registration")
                    .WithStep(m => m.UserRegistrationDetails, "details")
                        .AddControl(m => m.EmailAddress, "Email address", ControlType.Text).End()
                        .AddControl(m => m.Password, "Password", ControlType.HideableText).End()
                        .AddControl(m => m.ConfirmPassword, "Confirm password", ControlType.HideableText).End()
                    .End()
                    .WithStep(m => m.RegistrationDone, "done")
                    .End();                    
            moduleBuilder.AddFlowToModule("", "portal", "account", flow.Flow);
        }
    }
}
