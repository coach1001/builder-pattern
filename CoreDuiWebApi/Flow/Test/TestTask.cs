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
            taskData.Data.PersonalDetails.HasChildren = true;
            taskData.Data.PersonalDetails.Children = new List<Child>();
            taskData.Data.PersonalDetails.Children.Add(new Child());
            return Task.FromResult(taskData);
        }
    }
}
