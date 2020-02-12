using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CoreDuiWebApi.Email.Templates
{
    public class EmailTemplates : IEmailTemplates
    {
        private readonly AppConfig _appConfig;

        public EmailTemplates(IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig.Value;
        }
        public MailMessage BuildValidateEmail(string recipientEmailAddress, string validateToken)
        {
            throw new NotImplementedException();
        }

        public MailMessage BuildResetPasswordEmail(string recipientEmailAddress, string resetPasswordToken)
        {
            throw new NotImplementedException();
        }


    }
}