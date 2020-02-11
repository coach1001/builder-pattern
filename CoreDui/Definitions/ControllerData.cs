using System;
using System.Collections.Generic;
using System.Text;
using CoreDui.Enums;

namespace CoreDui.Definitions
{
    public class TaskData<TFlowDataType, TContextType>
    {
        public TFlowDataType Data { get; set; }
        public TContextType Context { get; set; }
        public TaskTypeEnum TaskType { get; set; }
        public string TaskPath { get; set; }
    }
}
