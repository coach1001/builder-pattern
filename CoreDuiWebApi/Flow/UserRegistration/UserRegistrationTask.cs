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

namespace CoreDuiWebApi.Flow.UserRegistration
{
    public class UserRegistrationTask : IFlowTask<UserRegistrationModel, UserRegistrationContext>
    {

        private readonly IAuthenticationService<DbUserClient> _authDbService;        
        private readonly IEmailTemplates _emailTemplates;        

        public UserRegistrationTask(
            IAuthenticationService<DbUserClient> authDbService,            
            IEmailTemplates emailTemplates
            )
        {
            _authDbService = authDbService;            
            _emailTemplates = emailTemplates;            
        }

        public async Task<TaskData<UserRegistrationModel, UserRegistrationContext>> Execute(TaskData<UserRegistrationModel, UserRegistrationContext> taskData)
        {
            taskData.Context.RegistrationEmailAddress = taskData.Data.UserRegistrationDetails.EmailAddress;

            if (taskData.Data.UserRegistrationDetails.Password != taskData.Data.UserRegistrationDetails.ConfirmPassword)
            {
                taskData.Context.UpdatedAt = DateTime.UtcNow;                
                taskData.Context.UserWithEmailAddressAlreadyExists = true;
                taskData.Context.VerificationEmailSent = false;
                return taskData;
            }

            var registerUserResult = await _authDbService.RegisterUser(new RegisterUser 
            { 
                EmailAddress = taskData.Data.UserRegistrationDetails.EmailAddress,
                Password = taskData.Data.UserRegistrationDetails.Password,
                ConfirmPassword = taskData.Data.UserRegistrationDetails.ConfirmPassword
            });

            if (registerUserResult.UserCreated)
            {
                try
                {                    
                    await _emailTemplates.BuildAndSendValidateEmail(
                        registerUserResult.EmailAddress,
                        registerUserResult.UserId.ToString(),
                        registerUserResult.ValidationToken.ToString());
                    taskData.Context.UpdatedAt = DateTime.UtcNow;                    
                    taskData.Context.VerificationEmailSent = true;
                    return taskData;
                }
                catch
                {
                    taskData.Context.UpdatedAt = DateTime.UtcNow;                    
                    taskData.Context.VerificationEmailSent = false;
                    return taskData;
                }
            }
            else
            {
                taskData.Context.UpdatedAt = DateTime.UtcNow;                
                taskData.Context.UserWithEmailAddressAlreadyExists = true;
                taskData.Context.VerificationEmailSent = false;
                return taskData;
            }
        }
    }
}
