using System;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.TMH1.A2A3A4
{
    public class A2A3A4CalculationTask : IFlowTask<A2A3A4Model, A2A3A4Context>
    {
        public A2A3A4CalculationTask()
        {
        }

        public async Task<TaskData<A2A3A4Model, A2A3A4Context>> Execute(TaskData<A2A3A4Model, A2A3A4Context> taskData)
        {
            return await Task.FromResult(taskData);
        }

        public decimal TruncateDecimal(decimal value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            decimal tmp = Math.Truncate(step * value);
            return tmp / step;
        }
    }
}
