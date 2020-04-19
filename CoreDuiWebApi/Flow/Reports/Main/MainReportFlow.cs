using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Builders;
using CoreDui.Enums;
using CoreDuiWebApi.Flow.Constants;
using CoreDuiWebApi.Flow.TMH1.A1.FlowConstants;

namespace CoreDuiWebApi.Flow.Reports
{
    public class MainReportFlow
    {
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder.WithFlow<MainReportModel, MainReportContext>("tmh1-main-report")
                 .RequiresAuthorization()
                 .WithReport(m => m.Data, "TMH1 Report")
                    .AddPage(m => m.Page1, "Page 1")
                    .End()
                    .AddPage(m => m.Page2, "Page 2")
                    .Orientation(PageOrientation.Landscape)
                    .End()
                    .AddPage(m => m.Page3, "Page 3")
                    .End()
                 .End();            
            moduleBuilder.AddFlowToModule("", "lab-calculator", "reports", flow.Flow);
        }
    }
}
