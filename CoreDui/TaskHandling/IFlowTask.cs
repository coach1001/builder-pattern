﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.Enums;

namespace CoreDui.TaskHandling
{
    public interface IFlowTask
    {
        Task<TaskData<TFlowDataType, TContextType>> Run<TFlowDataType, TContextType>(TaskData<TFlowDataType, TContextType> data);
        void SetTaskType(TaskTypeEnum taskType);
        TaskTypeEnum GetTaskType();
    }
}