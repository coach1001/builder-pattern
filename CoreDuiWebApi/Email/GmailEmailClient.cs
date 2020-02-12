using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
