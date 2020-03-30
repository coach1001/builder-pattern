
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
                        .ConfigTracks(new[] { GridMediaSize.Medium, GridMediaSize.Small }, "2fr 4fr")
                        .Next("Register")
                        .AddControl(m => m.EmailAddress, ControlType.Text, "Email address")
                            .ConfigSpans(new []{ GridMediaSize.Medium, GridMediaSize.Small}, 2)                            
                        .End()
                        .AddControl(m => m.MobilePrefix, ControlType.Select, "Prefix")                            
                            .WithOptions(new List<SelectOption>
                            {
                                new SelectOption { Key = "27", Display = "+27" },
                                new SelectOption { Key = "264", Display = "+264" },
                            })                            
                        .End()
                        .AddControl(m => m.MobileNumber, ControlType.Number, "Mobile number")
                        .End()
                        .AddControl(m => m.FirstName, ControlType.Text, "First name")
                            .ConfigSpans(new[] { GridMediaSize.Medium, GridMediaSize.Small }, 2)
                        .End()
                        .AddControl(m => m.Surname, ControlType.Text, "Surname")
                            .ConfigSpans(GridMediaSize.Large, 2)
                            .ConfigSpans(new[] { GridMediaSize.Medium, GridMediaSize.Small }, 2)
                        .End()
                        .AddControl(m => m.Password, ControlType.HideableText, "Password")
                            .ConfigSpans(GridMediaSize.Small, 2)
                        .End()
                        .AddControl(m => m.ConfirmPassword, ControlType.HideableText, "Confirm password")                                
                            .ConfigSpans(GridMediaSize.Large, 2)
                            .ConfigSpans(new[] { GridMediaSize.Medium, GridMediaSize.Small }, 2)
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
