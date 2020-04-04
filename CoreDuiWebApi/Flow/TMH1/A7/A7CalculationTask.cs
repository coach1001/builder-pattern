using System;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.TMH1.A7
{
    public class A7CalculationTask : IFlowTask<A7Model, A7Context>
    {
        public A7CalculationTask()
        {
        }

        public async Task<TaskData<A7Model, A7Context>> Execute(TaskData<A7Model, A7Context> taskData)
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
