using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CoreDuiWebApi.Email
{
    public class GmailEmailClient : IEmailClient
    {
        private readonly SmtpConfig _smtpConfig;

        public GmailEmailClient(IOptions<SmtpConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;            
        }

        public async Task<SendEmailResult> SendEmail(MailMessage message)
        {
            SmtpClient client = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password)
            };
            
            try
            {
                await client.SendMailAsync(message);                
                client.Dispose();
                return new SendEmailResult
                {
                    Sent = true,
                    Reason = ""
                };
            }
            catch(Exception ex)
            {
                client.Dispose();
                return new SendEmailResult
                {
                    Sent = false,
                    Reason = "Something went wrong."
                };
            }            
        }
    }
}
