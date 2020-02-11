using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.Enums;

namespace CoreDui.TaskHandling
{
    public class FlowTaskHandler : IFlowTask
    {
        public TaskTypeEnum TaskType { get; set;}
        public virtual Task<TaskData<TFlowDataType, TContextType>> Run<TFlowDataType, TContextType>(TaskData<TFlowDataType, TContextType> taskData)            
        {
            return Task.FromResult(taskData);
        }

        public void SetTaskType(TaskTypeEnum taskType)
        {
            TaskType = taskType;
        }

        public TaskTypeEnum GetTaskType()
        {
            return TaskType;
        }

    }
}
