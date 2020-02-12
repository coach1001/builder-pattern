using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreDuiWebApi.Email.Templates
{
    public interface IEmailTemplates
    {
        Task BuildAndSendValidateEmail(string recipientEmailAddress, string accountId, string validateToken);
        Task BuildAndSendResetPasswordEmail(string recipientEmailAddress, string accountId, string resetPasswordToken);
    }
}
