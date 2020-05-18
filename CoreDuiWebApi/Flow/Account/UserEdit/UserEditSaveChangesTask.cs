using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.Account.UserEdit
{
    public class UserEditSaveChangesTask : IFlowTask<UserEditModel, UserEditContext>
    {
        public UserEditSaveChangesTask()
        {
        }

        public async Task<TaskData<UserEditModel, UserEditContext>> Execute(TaskData<UserEditModel, UserEditContext> taskData)
        {
            return await Task.FromResult(taskData);
        }
    }
}
