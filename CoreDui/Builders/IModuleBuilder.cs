using System;
using System.Collections.Generic;
using System.Text;
using CoreDui.Definitions;

namespace CoreDui.Builders
{
    public interface IModuleBuilder
    {
        FlowBuilder<TFlowDataType, TContextDataType> WithFlow<TFlowDataType, TContextDataType>(string name);
        void AddFlowToModule(string route, string system, string module, FlowDefinition definition);
    }
}
