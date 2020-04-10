using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.TaskHandling;

namespace CoreDuiWebApi.Flow.Test
{
    public class TestTask : IFlowTask<TestData, TestContext>
    {
        public Task<TaskData<TestData, TestContext>> Execute(TaskData<TestData, TestContext> taskData)
        {
            if(taskData.Data.PersonalDetails.Children != null)
            {
                taskData.Data.PersonalDetails.Children.Clear();
            }
            return Task.FromResult(taskData);
        }
    }
}
