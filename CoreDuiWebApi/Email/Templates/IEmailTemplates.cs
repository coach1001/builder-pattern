using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreDuiWebApi.Email.Templates
{
    public interface IEmailTemplates
    {
        MailMessage BuildValidateEmail(string recipientEmailAddress, string validateToken);
        MailMessage BuildResetPasswordEmail(string recipientEmailAddress, string resetPasswordToken);
    }
}
