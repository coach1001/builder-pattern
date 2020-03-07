
using System.Collections.Generic;
using CoreDui.Builders;
using CoreDui.Definitions;
using CoreDui.Enums;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.Account.UserRegistration
{
    public static class UserRegistrationFlow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<UserRegistrationModel, UserRegistrationContext>("user-registration")
                    .WithStep(m => m.UserRegistrationDetails, "details", "create")    
                        .Next("Register")
                        .AddControl(m => m.EmailAddress, "Email address", ControlType.Text)
                            .WithLayout(50, 100)
                        .End()
                        .AddControl(m => m.MobilePrefix, "Prefix", ControlType.Select)                            
                            .WithLayout(20, 40)
                            .WithOptions(new List<SelectOption>
                            {
                                new SelectOption { Key = "27", Display = "+27" },
                                new SelectOption { Key = "264", Display = "+264" },
                            })
                        .End()
                        .AddControl(m => m.MobileNumber, "Mobile number", ControlType.Number)
                            .WithLayout(30, 60)
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
                        .WithTask<UserRegistrationTask>(TaskTypeEnum.PostTask)
                    .End()
                    .WithStep(m => m.RegistrationDone, "done", "done")
                        .Next("Done")
                    .End();                    
            moduleBuilder.AddFlowToModule("", "portal", "account", flow.Flow);
        }
    }
}
