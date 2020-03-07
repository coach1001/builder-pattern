using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CoreDui.Definitions;
using CoreDui.Repositories;
using CoreDui.Extensions;
using CoreDui.Utils;
using CoreDui.Enums;
using CoreDui.TaskHandling;

namespace CoreDui.Builders
{
    public class CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType> 
    {
        public Element Element { get; set; }
        public TParentType Parent { get; set; }
        
        private readonly IElementTypeTemplateMapper _elementMapper;
        private readonly IControlTypeTemplateMapper _controlMapper;
        private readonly IValidationAttributeJsConverterMapper _validationMapper;

        public CollectionBuilder(TParentType parent, string name,            
            IElementTypeTemplateMapper elementMapper,
            IControlTypeTemplateMapper controlMapper,
            IValidationAttributeJsConverterMapper validationMapper,
            ElementType elementType = ElementType.Object)
        {
            Parent = parent;            
            _elementMapper = elementMapper;
            _controlMapper = controlMapper;
            _validationMapper = validationMapper;

            Element = new Element
            {
                Name = name,
                ElementType = elementType,
                DataType = typeof(TDataType),                
                Elements = new List<Element>(),
                UiTemplate = elementMapper.GetDefault(elementType),
                ControlType = null
            };
        }

        public CollectionBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDerived, TContextType>
            AddGroup<TDerived>(Expression<Func<TDataType, TDerived>> property, string name = null)
        {
            var expression = (MemberExpression)property.Body;
            var attrs = expression.Member.GetCustomAttributes(false);
            var validators = GenerateValidationModel.Generate(attrs, _validationMapper);
            var modelProperty = expression.Member.Name.FirstCharToLower();
            name = name != null ? name : modelProperty;             
            
            var builder = new CollectionBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDerived, TContextType>
                (this, name, _elementMapper, _controlMapper, _validationMapper);

            builder.Element.ModelProperty = modelProperty;
            builder.Element.TaskPath = $"{Element.TaskPath}.{modelProperty}";
            builder.Element.DataType = property.ReturnType;            
            builder.Element.UiTemplate = _elementMapper.GetDefault(ElementType.Object);
            builder.Element.Tasks = new List<TaskDefinition>
            {
                
            };
            if (validators != null)
            {
                builder.Element.Validators = validators;
            }
            Element.Elements.Add(builder.Element);
            return builder;
        }

        public CollectionBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDerived, TContextType>
            AddArray<TDerived>(Expression<Func<TDataType, ICollection<TDerived>>> property, string name = null)
        {
            var expression = (MemberExpression)property.Body;
            var attrs = expression.Member.GetCustomAttributes(false);
            var validators = GenerateValidationModel.Generate(attrs, _validationMapper);
            var modelProperty = expression.Member.Name.FirstCharToLower();
            name = name != null ? name : modelProperty;

            var builder = new CollectionBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDerived, TContextType>(
                this, name, _elementMapper, _controlMapper, _validationMapper, ElementType.Array);
            
            builder.Element.ModelProperty = modelProperty;
            builder.Element.TaskPath = $"{Element.TaskPath}.{modelProperty}[]";
            builder.Element.DataType = property.ReturnType.GetGenericArguments()[0];            
            builder.Element.UiTemplate = _elementMapper.GetDefault(ElementType.Array);
            if (validators != null)
            {
                builder.Element.Validators = validators;
            }
            Element.Elements.Add(builder.Element);
            return builder;
        }

        public ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDataType,  TContextType>
            AddControl<TPropertyType>(Expression<Func<TDataType, TPropertyType>> property, string name = null, ControlType controlType = ControlType.Text)
        {            
            var expression = (MemberExpression)property.Body;
            var attrs = expression.Member.GetCustomAttributes(false);
            var validators = GenerateValidationModel.Generate(attrs, _validationMapper);
            var modelProperty = expression.Member.Name.FirstCharToLower();
            name = name != null ? name : modelProperty;

            var builder = new ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDataType, TContextType>(
                this, name, _controlMapper, controlType);

            builder.Element.ModelProperty = modelProperty;
            builder.Element.TaskPath = $"{Element.TaskPath}.{modelProperty}";
            builder.Element.DataType = property.ReturnType;            
            builder.Element.UiTemplate = _controlMapper.GetDefault(controlType);
            if (validators != null)
            {
                builder.Element.Validators = validators;
            }
            Element.Elements.Add(builder.Element);
            return builder;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>
            WithReactivity<TCustomType>(Expression<Func<TCustomType, bool>> ex, ReactivityType reactivityType)
        {
            var linqString = new LinqJsString();
            linqString.Visit(ex);
            var output = linqString.sb.ToString();
            return this;            
        }

        public CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>
            WithLayout(int stretchLarge, int stretchSmall)
        {
            Element.Layout.StretchLarge = stretchLarge;
            Element.Layout.StretchSmall = stretchSmall;
            return this;
        }


        public CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType> Back(string text)
        {
            Element.BackButton = text;
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType> Next(string text)
        {
            Element.NextButton = text;
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>
            WithTask<TFlowTask>(TaskTypeEnum type)
            where TFlowTask : IFlowTask<TFlowDataType, TContextType>
        {
            if (Element.Tasks == null)
            {
                Element.Tasks = new List<TaskDefinition>();
            }
            var task = new TaskDefinition
            {
                Type = typeof(TFlowTask),
                TaskType = type,
                RequiresValidDateToExecute = false
            };            
            Element.Tasks.Add(task);
            return this;
        }

        public TParentType End()
        {
            return Parent;
        }

        public Element Build()
        {
            return Element;
        }
    }
}
