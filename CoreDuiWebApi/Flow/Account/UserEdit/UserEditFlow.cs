using CoreDui.Builders;
using CoreDui.Enums;

namespace CoreDuiWebApi.Flow.Account.UserEdit
{
    public static class UserEditFlow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<UserEditModel, UserEditContext>("user-edit")
                    .WithStep(m => m.UserGrid, "select-user", "edit")                        
                        .WithTask<UserEditGetGridTask>(TaskTypeEnum.PreTask)
                        .Next("Next")
                        .AddArray(m => m.Users, "System Users", "defaultGrid")
                            .WithMetadata("editable", false)
                            .WithMetadata("allColumnsSearchable", true)
                            .WithMetadata("allColumnsOrderBy", true)
                            .GridConfig("repeat(9, 1fr)")
                            .HideAddAndDelete()
                            .AddControl(m => m.Id, ControlType.Column, "Id").InitiallyDisabled().End()
                            .AddControl(m => m.ProviderId, ControlType.Column, "Provider Id").InitiallyDisabled().End()
                            .AddControl(m => m.ProviderType, ControlType.Column, "Provider Type").InitiallyDisabled().End()
                            .AddControl(m => m.DisplayName, ControlType.Column, "Display Name").InitiallyDisabled().End()
                            .AddControl(m => m.FirstName, ControlType.Column, "First Name").InitiallyDisabled().End()
                            .AddControl(m => m.LastName, ControlType.Column, "Last Name").InitiallyDisabled().End()
                            .AddControl(m => m.EmailAddress, ControlType.Column, "Email Address").InitiallyDisabled().End()
                            .AddControl(m => m.AccountEnabled, ControlType.Column, "Enabled").InitiallyDisabled().End()
                            .AddControl(m => m.Id, ControlType.Button, "Edit")                              
                                .WithMetadata("icon", "edit")
                                .WithUiTask(TaskTypeEnum.PeriTask, "user-edit") // BUTTON CLICK
                            .End()
                        .End()
                    .End()
                    .WithStep(m => m.UserEdit, "edit", "edit")
                        .WithTask<UserEditRetrieveUserTask>(TaskTypeEnum.PreTask)
                        .Back("Back")
                        .GridConfig("1fr 1fr")
                        .AddControl(m => m.Id, ControlType.Text, "Id").End()
                        .AddControl(m => m.ProviderId, ControlType.Text, "Login Id").End()
                        .AddControl(m => m.ProviderType, ControlType.Text, "Provider Type").End()
                        .AddControl(m => m.DisplayName, ControlType.Text, "Display name").End()
                        .AddControl(m => m.FirstName, ControlType.Text, "First name").End()
                        .AddControl(m => m.LastName, ControlType.Text, "Last name").End()
                        .AddControl(m => m.EmailAddress, ControlType.Text, "Email address").End()
                        .AddControl(m => m.AccountEnabled, ControlType.Boolean, "Account enabled").End()
                        .AddControl(m => m.Id, ControlType.Button, "save-changes")
                            .WithMetadata("icon", "save")
                            .WithTask<UserEditSaveChangesTask>(TaskTypeEnum.PeriTask, "user-edit")
                        .End()
                    .End();
            moduleBuilder.AddFlowToModule("", "lab-calculator", "account", flow.Flow);
        }
    }
}
