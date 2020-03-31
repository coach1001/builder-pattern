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

        public async Task<TaskData<A1Model, A1Context>> Execute(TaskData<A1Model, A1Context> taskData)
        {
            if(taskData.Data.Data.TotalSampleMass > 0 && taskData.Data.Data.RiffledDryMass > 0)
            {
                taskData.Data.Data.ReductionFactor =  taskData.Data.Data.TotalSampleMass / taskData.Data.Data.RiffledDryMass;
            } else
            {
                taskData.Data.Data.ReductionFactor = null;
            }            
            return await Task.FromResult(taskData);
        }
    }
}
