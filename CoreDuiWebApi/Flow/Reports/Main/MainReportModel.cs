using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Attributes;
using CoreDui.Definitions;
using CoreDui.JsonSerializers.Collection;
using Newtonsoft.Json;

namespace CoreDuiWebApi.Flow.Reports
{
    public class MainReportModel
    {
        public MainReportData Data { get; set; }
    }

    public class MainReportData
    {
        public Page1 Page1 { get; set; }
        public Page2 Page2 { get; set; }
        public Page3 Page3 { get; set; }
    }

    public class Page1
    {
    }

    public class Page2
    {
    }

    public class Page3
    {
    }

}
