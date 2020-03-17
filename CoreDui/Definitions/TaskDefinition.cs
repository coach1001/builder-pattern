using System;
using System.Collections.Generic;
using System.Text;
using CoreDui.Enums;
using Newtonsoft.Json;

namespace CoreDui.Definitions
{
    public class TaskDefinition
    {
        [JsonIgnore]
        public Type Type { get; set; }
        public bool RequiresValidDataToExecute { get; set; }
        public TaskTypeEnum TaskType { get; set; }

        public TaskTypeEnum GetTaskType()
        {
            return TaskType;
        }
    }
}
