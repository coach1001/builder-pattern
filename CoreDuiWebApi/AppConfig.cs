using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDuiWebApi
{
    public class AppConfig
    {
        public string Name { get; set; }
        public string ApiHost { get; set; }
        public int ApiPort { get; set; }
        public string ApiBasePath { get; set; }
        public string UiHost { get; set; }
        public string UiPort { get; set; }
        public string CommsFromEmail { get; set; }
        public string ApiAccountValidatePath { get; set; }
        public string UiAccountValidationSuccessPath { get; set; }
        public string UiAccountValidationFailedPath { get; set; }
    }
}
