using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CoreDuiWebApi.Email
{
    public interface IEmailClient
    {
        Task<SendEmailResult> SendEmail(MailMessage message);
    }
}
