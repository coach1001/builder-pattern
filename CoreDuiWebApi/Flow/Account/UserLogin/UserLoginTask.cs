using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.Enums;
using CoreDui.TaskHandling;
using CoreDuiWebApi.Authentication;
using CoreDuiWebApi.Email.Templates;
using Microsoft.Extensions.Options;

namespace CoreDuiWebApi.Flow.Account.UserLogin
{
    public class UserLoginTask : IFlowTask<UserLoginModel, UserLoginContext>
    {
        private readonly IAuthenticationService<DbUserClient> _authDbService;
        private readonly IAuthenticationService<LdapUser> _authLdapService;
        private readonly IEmailTemplates _emailTemplates;
        private readonly AppConfig _appConfig;

        public UserLoginTask(
            IAuthenticationService<DbUserClient> authDbService,
            IAuthenticationService<LdapUser> authLdapService,
            IEmailTemplates emailTemplates,
            IOptions<AppConfig> appConfig)
        {
            _authDbService = authDbService;
            _authLdapService = authLdapService;
            _emailTemplates = emailTemplates;
            _appConfig = appConfig.Value;
        }

        public async Task<TaskData<UserLoginModel, UserLoginContext>> Execute(TaskData<UserLoginModel, UserLoginContext> taskData)
        {
            var ldapUser = await _authLdapService.Login(
                taskData.Data.UserLoginDetails.UsernameOrEmailAddress,
                taskData.Data.UserLoginDetails.Password);

            if (ldapUser != null)
            {
                taskData.Context.LoginSuccessfull = true;
                taskData.Context.UpdatedAt = DateTime.UtcNow;
                taskData.Context.DbUser = null;
                taskData.Context.LdapUser = ldapUser;                
            }
            else
            {
                var dbUser = await _authDbService.Login(
                    taskData.Data.UserLoginDetails.UsernameOrEmailAddress,
                    taskData.Data.UserLoginDetails.Password);
                if (dbUser != null)
                {
                    taskData.Context.LoginSuccessfull = true;
                    taskData.Context.UpdatedAt = DateTime.UtcNow;
                    taskData.Context.LdapUser = null;
                    taskData.Context.DbUser = dbUser;                    
                }
            }
            return taskData;
        }
    }
}
