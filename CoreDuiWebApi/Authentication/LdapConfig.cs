using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDuiWebApi.Authentication
{
    public class LdapConfig
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string BindDn { get; set; }
        public string BindPassword { get; set; }
        public string SearchDn { get; set; }
        public string Filter { get; set; }
    }
}
