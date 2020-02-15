
using System.Collections.Generic;
using CoreDui.Builders;
using CoreDui.Definitions;
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
                        .AddControl(m => m.EmailAddress, "Email address", ControlType.Text)
                            .WithLayout(50, 100)
                        .End()
                        .AddControl(m => m.MobilePrefix, "Prefix", ControlType.Select)                            
                            .WithLayout(20, 20)
                            .WithOptions(new List<SelectOption>
                            {
                                new SelectOption { Key = "27", Display = "+27" },
                                new SelectOption { Key = "264", Display = "+264" },
                            })
                        .End()
                        .AddControl(m => m.MobileNumber, "Mobile number", ControlType.Number)
                            .WithLayout(30, 80)
                        .End()
                        .AddControl(m => m.FirstName, "First name", ControlType.Text)
                            .WithLayout(50, 100)
                        .End()
                        .AddControl(m => m.Surname, "Surname", ControlType.Text)
                            .WithLayout(50, 100)
                        .End()
                        .AddControl(m => m.Password, "Password", ControlType.HideableText)
                            .WithLayout(50, 100)
                        .End()
                        .AddControl(m => m.ConfirmPassword, "Confirm password", ControlType.HideableText)
                            .WithLayout(50, 100)
                        .End()
                    .End()
                    .WithStep(m => m.RegistrationDone, "done")
                    .End();                    
            moduleBuilder.AddFlowToModule("", "portal", "account", flow.Flow);
        }
    }
}
