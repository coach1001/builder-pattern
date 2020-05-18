using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.Account.UserEdit
{
    public class UserEditRetrieveUserTask : IFlowTask<UserEditModel, UserEditContext>
    {
        public UserEditRetrieveUserTask()
        {
        }

        public async Task<TaskData<UserEditModel, UserEditContext>> Execute(TaskData<UserEditModel, UserEditContext> taskData)
        {
            return await Task.FromResult(taskData);
        }
    }
}
