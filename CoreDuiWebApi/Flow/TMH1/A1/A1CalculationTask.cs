using System;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.TMH1.A1
{
    public class A1CalculationTask : IFlowTask<A1Model, A1Context>
    {
        public A1CalculationTask()
        {
        }

        public Task<TaskData<A1Model, A1Context>> Execute(TaskData<A1Model, A1Context> taskData)
        {            
            if(taskData.Data.Data.TotalSampleMass > 0 && taskData.Data.Data.RiffledDryMass > 0)
            {
                taskData.Data.Data.ReductionFactor =  TruncateDecimal((taskData.Data.Data.TotalSampleMass / taskData.Data.Data.RiffledDryMass).Value, 3);
            } else
            {
                taskData.Data.Data.ReductionFactor = null;
            }
            return Task.FromResult(taskData);
        }

        public decimal TruncateDecimal(decimal value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            decimal tmp = Math.Truncate(step * value);
            return tmp / step;
        }
    }
}
