using System;
using System.Collections.Generic;
using System.Text;
using CoreDui.Enums;

namespace CoreDui.Definitions
{
    public class TaskDefinition
    {
        public Type Type { get; set; }
        public bool RequiresValidDateToExecute { get; set; }
        public TaskTypeEnum TaskType { get; set; }

        public TaskTypeEnum GetTaskType()
        {
            return TaskType;
        }
    }
}
