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
                    .End()
                    .WithStep(m => m.UserGrid, "Edit user", "edit")
                    .End();
            moduleBuilder.AddFlowToModule("", "lab-calculator", "account", flow.Flow);
        }
    }
}
