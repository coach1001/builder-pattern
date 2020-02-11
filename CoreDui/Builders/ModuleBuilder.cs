using System;
using System.Collections.Generic;
using System.Text;
using CoreDui.Definitions;
using CoreDui.Repositories;

namespace CoreDui.Builders
{
    public class ModuleBuilder : IModuleBuilder
    {        
        private readonly IModuleRepository _moduleRepository;
        private readonly IElementTypeTemplateMapper _elementMapper;
        private readonly IControlTypeTemplateMapper _controlMapper;
        private readonly IValidationAttributeJsConverterMapper _validationMapper;
    
        public ModuleBuilder(
            IModuleRepository moduleRepository,
            IElementTypeTemplateMapper elementMapper,
            IControlTypeTemplateMapper controlMapper,
            IValidationAttributeJsConverterMapper validationMapper
            )
        {
            _moduleRepository = moduleRepository;
            _elementMapper = elementMapper;
            _controlMapper = controlMapper;
            _validationMapper = validationMapper;
        }

        public FlowBuilder<TFlowDataType, TContextDataType> WithFlow<TFlowDataType, TContextDataType>(string name)
        {
            return new FlowBuilder<TFlowDataType, TContextDataType>(_elementMapper, _controlMapper, _validationMapper, name);
        }

        public void AddFlowToModule(string route, string system, string moduleName, FlowDefinition flow)
        {
            var moduleExists = _moduleRepository.Exists(route, system, moduleName);
            if(moduleExists)
            {
                _moduleRepository.AddFlowToExistingModule(route, system, moduleName, flow);
            }
            else
            {
                var module = new ModuleDefinition
                {
                    Route = route,
                    System = system,
                    ModuleName = moduleName,
                    Flows = new List<FlowDefinition>
                    {
                        flow
                    }
                };
                _moduleRepository.AddModule(module);
            }
        }
    }
}
