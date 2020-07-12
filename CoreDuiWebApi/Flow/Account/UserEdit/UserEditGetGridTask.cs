using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.Enums;
using CoreDui.TaskHandling;
using CoreDuiWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreDuiWebApi.Flow.Account.UserEdit
{
    public class UserEditGetGridTask : IFlowTask<UserEditModel, UserEditContext>
    {
        private readonly DbLabCalcContext _context;
        public UserEditGetGridTask(DbLabCalcContext context)
        {
            _context = context;
        }

        public async Task<TaskData<UserEditModel, UserEditContext>> Execute(TaskData<UserEditModel, UserEditContext> taskData)
        {
            var allUsers = await _context.DbUsers.ToListAsync();
            if (allUsers != null)
            {
                taskData.Data.UserGrid = new UserGrid
                {
                    Users = new List<User>()
                };
                foreach (var user in allUsers)
                {
                    taskData.Data.UserGrid.Users.Add(new User
                    {
                        Id__ = user.Id,
                        Operation__ = ArrayOperation.ReadOnly,
                        Id = user.Id,
                        ProviderId = user.ProviderId,
                        ProviderType = user.ProviderType,
                        DisplayName = user.DisplayName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailAddress = user.EmailAddress,
                        AccountEnabled = user.AccountEnabled
                    });
                }
            }
            return await Task.FromResult(taskData);
        }
    }
}
