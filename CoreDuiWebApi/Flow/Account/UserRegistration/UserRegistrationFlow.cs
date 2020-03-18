
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
                        .ConfigTracks(GridMediaSize.Large, "5fr 2fr 3fr")
                        .Next("Register")
                        .AddControl(m => m.EmailAddress, "Email address", ControlType.Text)
                        .End()
                        .AddControl(m => m.MobilePrefix, "Prefix", ControlType.Select)                            
                            .WithOptions(new List<SelectOption>
                            {
                                new SelectOption { Key = "27", Display = "+27" },
                                new SelectOption { Key = "264", Display = "+264" },
                            })
                        .End()
                        .AddControl(m => m.MobileNumber, "Mobile number", ControlType.Number)
                        .End()
                        .AddControl(m => m.FirstName, "First name", ControlType.Text)
                        .End()
                        .AddControl(m => m.Surname, "Surname", ControlType.Text)
                            .ConfigSpans(GridMediaSize.Large, 2)
                        .End()
                        .AddControl(m => m.Password, "Password", ControlType.HideableText)
                        .End()
                        .AddControl(m => m.ConfirmPassword, "Confirm password", ControlType.HideableText)
                            .ConfigSpans(GridMediaSize.Large, 2)
                        .End()
                        .WithTask<UserRegistrationTask>(TaskTypeEnum.PostTask)
                    .End()
                    .WithStep(m => m.RegistrationDone, "done", "done")
                        .Next("Done")
                    .End();                    
            moduleBuilder.AddFlowToModule("", "lab-calculator", "account", flow.Flow);
        }
    }
}
