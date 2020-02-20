using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreDui.Definitions;
using CoreDui.Enums;

namespace CoreDui.TaskHandling
{
    public interface IFlowTask<TFlowDataType, TContextType>
    {
        Task<TaskData<TFlowDataType, TContextType>> Execute(TaskData<TFlowDataType, TContextType> taskData);        
    }
}
