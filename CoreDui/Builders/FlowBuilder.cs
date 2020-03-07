﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CoreDui.Definitions;
using CoreDui.Enums;
using CoreDui.Extensions;
using CoreDui.Repositories;
using CoreDui.TaskHandling;
using CoreDui.Utils;

namespace CoreDui.Builders
{
    public class FlowBuilder<TFlowDataType, TContextType> : IFlowBuilder
        where TContextType : BaseContextModel
    {
        
        private readonly IElementTypeTemplateMapper _elementMapper;
        private readonly IControlTypeTemplateMapper _controlMapper;
        private readonly IValidationAttributeJsConverterMapper _validationMapper;

        public FlowDefinition Flow { get; set; }

        public FlowBuilder(
            IElementTypeTemplateMapper elementMapper,
            IControlTypeTemplateMapper controlMapper,
            IValidationAttributeJsConverterMapper validationMapper,
            string name
            )
        {
            _elementMapper = elementMapper;
            _controlMapper = controlMapper;
            _validationMapper = validationMapper;

            Flow = new FlowDefinition();
            Flow.TaskPath = typeof(TFlowDataType).Name.FirstCharToLower();
            Flow.DataType = typeof(TFlowDataType);
            Flow.ContextType = typeof(TContextType);
            Flow.Flow = name;
            Flow.Steps = new List<Element>();
        }

        public CollectionBuilder<TFlowDataType, FlowBuilder<TFlowDataType, TContextType>, TDerived, TContextType> 
            WithStep<TDerived>(Expression<Func<TFlowDataType, TDerived>> property, string name = null, string icon = null)
        {
            var expression = (MemberExpression)property.Body;
            var attrs = expression.Member.GetCustomAttributes(false);
            var validators = GenerateValidationModel.Generate(attrs, _validationMapper);
            var modelProperty = expression.Member.Name.FirstCharToLower();
            name = name != null ? name : modelProperty;

            var builder = new CollectionBuilder<TFlowDataType, FlowBuilder<TFlowDataType, TContextType>, TDerived, TContextType>
                (this, name, _elementMapper, _controlMapper, _validationMapper, ElementType.Object);

            builder.Element.ModelProperty = modelProperty;
            builder.Element.TaskPath = $"{Flow.TaskPath}.{modelProperty}";
            builder.Element.DataType = property.ReturnType;
            builder.Element.UiTemplate = _elementMapper.GetDefault(ElementType.Object);
            builder.Element.Icon = icon;
            builder.WithTask<DefaultFlowPersistanceTask<TFlowDataType, TContextType>>(TaskTypeEnum.PostTask);

            if (validators != null)
            {
                builder.Element.Validators = validators;
            }
            Flow.Steps.Add(builder.Element);
            return builder;
        }

        public FlowDefinition Build()
        {
            return Flow;
        }

    }
}
