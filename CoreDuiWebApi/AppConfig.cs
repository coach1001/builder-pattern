using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDuiWebApi
{
    public class AppConfig
    {
        public string ApiName { get; set; }
        public string ApiBaseUrl { get; set; }
        public string ApiPort { get; set; }
        public string UiName { get; set; }
        public string UiBaseUrl { get; set; }
        public string UiPort { get; set; }
        public string CommsFromEmail { get; set; }
    }
}
