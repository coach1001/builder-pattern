using System;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.Reports
{
    public class MainReportTask : IFlowTask<MainReportModel, MainReportContext>
    {
        public MainReportTask()
        {
        }

        public Task<TaskData<MainReportModel, MainReportContext>> Execute(TaskData<MainReportModel, MainReportContext> taskData)
        {            
            return Task.FromResult(taskData);
        }
    }
}
