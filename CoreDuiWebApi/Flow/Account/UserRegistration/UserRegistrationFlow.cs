
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
                        .GridConfig("5fr 2fr 3fr")
                        .AddControl(m => m.EmailAddress, ControlType.Text, "Email address")
                            .PositionConfig("1 / 2", "1 / 1")
                        .End()
                        .AddControl(m => m.MobilePrefix, ControlType.Select, "Prefix")
                            .PositionConfig("2 / 3", "1 / 1")
                            .WithOptions(new List<SelectOption>
                            {
                                new SelectOption { Key = "27", Display = "+27" },
                                new SelectOption { Key = "264", Display = "+264" },
                            })                            
                        .End()
                        .AddControl(m => m.MobileNumber, ControlType.Number, "Mobile number")
                            .PositionConfig("3 / 4", "1 / 1")                            
                        .End()
                        .AddControl(m => m.FirstName, ControlType.Text, "First name")
                            .PositionConfig("1 / 2", "2 / 2")
                        .End()
                        .AddControl(m => m.Surname, ControlType.Text, "Surname")
                            .PositionConfig("2 / 4", "2 / 2")
                        .End()
                        .AddControl(m => m.Password, ControlType.HideableText, "Password")
                            .PositionConfig("1 / 2", "3 / 3")
                        .End()
                        .AddControl(m => m.ConfirmPassword, ControlType.HideableText, "Confirm password")
                            .PositionConfig("2 / 4", "3 / 3")
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
