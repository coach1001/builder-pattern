using System.Linq;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.TaskHandling;
using CoreDuiWebApi.Data;

namespace CoreDuiWebApi.Flow.Account.UserEdit
{
    public class UserEditRetrieveUserTask : IFlowTask<UserEditModel, UserEditContext>
    {
        private readonly DbLabCalcContext _context;

        public UserEditRetrieveUserTask(DbLabCalcContext context)
        {
            _context = context;
        }

        public async Task<TaskData<UserEditModel, UserEditContext>> Execute(TaskData<UserEditModel, UserEditContext> taskData)
        {
            var dbUser = _context.DbUsers.FirstOrDefault(m => m.Id == taskData.Context.EditingUser);
            taskData.Data.UserEdit = new User
            {
                Id = dbUser.Id,
                ProviderId = dbUser.ProviderId,
                ProviderType = dbUser.ProviderType,
                DisplayName = dbUser.DisplayName,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                EmailAddress = dbUser.EmailAddress,
                AccountEnabled = dbUser.AccountEnabled
            };
            return await Task.FromResult(taskData);
        }
    }
}
