using System;
using System.Net.Mail;
using System.Threading.Tasks;
using CoreDuiWebApi.Email.DbEmailEf;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoreDuiWebApi.Email.Templates
{
    public class EmailTemplates : IEmailTemplates
    {
        private readonly AppConfig _appConfig;
        private readonly DbLabCalcContext _context;
        private readonly IEmailClient _emailClient;        
        public EmailTemplates(
            IOptions<AppConfig> appConfig,
            DbLabCalcContext context,
            IEmailClient emailClient)
        {
            _appConfig = appConfig.Value;
            _context = context;
            _emailClient = emailClient;
        }
        public async Task BuildAndSendValidateEmail(string recipientEmailAddress, string accountId, string validationToken)
        {
            var body = $"{_appConfig.Name} - Click link to validate and enable your account\n";
            
            body += 
                $"\n {_appConfig.ApiHost}:{_appConfig.ApiPort.ToString()}/{_appConfig.ApiBasePath}/" + 
                $"{_appConfig.ApiAccountValidatePath}/{accountId}?validationToken={validationToken} \n";
            
            body += "\nThank you\n";

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_appConfig.CommsFromEmail);
            mailMessage.To.Add(recipientEmailAddress);
            mailMessage.Body = body;
            mailMessage.Subject = $"{_appConfig.Name} - Validate and Enable your Account";
            
            await SendMail(mailMessage);
        }

        public Task BuildAndSendResetPasswordEmail(string recipientEmailAddress, string accountId, string resetPasswordToken)
        {
            throw new NotImplementedException();
        }

        private async Task SendMail(MailMessage mailMessage)
        {
            var mailMessageString = JsonConvert.SerializeObject(mailMessage);
            var dbEmail = new DbEmail
            {
                Message = mailMessageString,
                Status = "PROCESSING",
                RetryCount = 0
            };
            _context.DbEmails.Add(dbEmail);
            await _context.SaveChangesAsync();
            var result = await _emailClient.SendEmail(mailMessage);
            if (result.Sent)
            {
                dbEmail.UpdatedAt = DateTime.UtcNow;
                dbEmail.Status = "SENT";
            }
            else
            {
                dbEmail.UpdatedAt = DateTime.UtcNow;
                dbEmail.Status = "FAILED_REQUEUED";
                dbEmail.RetryCount += 1;
            }
            _context.DbEmails.Update(dbEmail);
            await _context.SaveChangesAsync();
        }

    }
}