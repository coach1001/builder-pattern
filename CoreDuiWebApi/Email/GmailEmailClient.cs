using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CoreDuiWebApi.Email
{
    public class GmailEmailClient : IEmailClient
    {
        private readonly GmailConfig _smtpConfig;

        public GmailEmailClient(IOptions<GmailConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
    }
}
