using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CoreDui.Definitions;
using CoreDui.Repositories;
using CoreDui.Extensions;
using CoreDui.Utils;
using CoreDui.Enums;
using CoreDui.TaskHandling;
using shortid;

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
            // name = name != null ? name : modelProperty;

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
            // name = name != null ? name : modelProperty;

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

        public ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDataType, TContextType>
            AddControl<TPropertyType>(Expression<Func<TDataType, TPropertyType>> property, ControlType controlType = ControlType.Text, string name = null)
        {
            var expression = (MemberExpression)property.Body;
            var attrs = expression.Member.GetCustomAttributes(false);
            var validators = GenerateValidationModel.Generate(attrs, _validationMapper);
            var modelProperty = expression.Member.Name.FirstCharToLower();
            // name = name != null ? name : modelProperty;

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

        public ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDataType, TContextType>
            AddDecorator(string name, ControlType controlType = ControlType.Decorator)
        {
            
            var builder = new ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDataType, TContextType>(
                this, name, _controlMapper, controlType);
            builder.Element.ModelProperty = ShortId.Generate(false, false, 14);
            builder.Element.UiTemplate = _controlMapper.GetDefault(controlType);
            Element.Elements.Add(builder.Element);
            return builder;
        }

        public ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDataType, TContextType>
            AddSpacer(ControlType controlType = ControlType.Spacer)
        {
            var builder = new ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>, TDataType, TContextType>(
                this, null, _controlMapper, controlType);
            builder.Element.ModelProperty = ShortId.Generate(false, false, 14);
            builder.Element.UiTemplate = _controlMapper.GetDefault(controlType);            
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
                RequiresValidDataToExecute = false
            };
            Element.Tasks.Add(task);
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>
           ConfigSpans(
                GridMediaSize gridMediaSize,
                int colSpan,
                int rowSpan = 1
           )
        {
            if (Element.GridConfig.SpanConfig == null)
            {
                Element.GridConfig.SpanConfig = new Dictionary<string, SpanConfig>();
            }

            Element.GridConfig.SpanConfig[gridMediaSize.ToString().ToLower()] = new SpanConfig
            {
                Columns = colSpan,
                Rows = rowSpan
            };
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>
            ConfigSpans(
                GridMediaSize[] gridMediaSizes,
                int colSpan,
                int rowSpan = 1
            )
        {
            if (Element.GridConfig.SpanConfig == null)
            {
                Element.GridConfig.SpanConfig = new Dictionary<string, SpanConfig>();
            }

            foreach (GridMediaSize gridMediaSize in gridMediaSizes)
            {
                Element.GridConfig.SpanConfig[gridMediaSize.ToString().ToLower()] = new SpanConfig
                {
                    Columns = colSpan,
                    Rows = rowSpan
                };
            }
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>
            ConfigTracks(
                GridMediaSize gridMediaSize,
                string columnsTrackConfig = "",
                string rowsTrackConfig = ""
            )
        {
            if (Element.GridConfig.TrackConfig == null)
            {
                Element.GridConfig.TrackConfig = new Dictionary<string, TrackConfig>();
            }

            Element.GridConfig.TrackConfig[gridMediaSize.ToString().ToLower()] = new TrackConfig
            {
                Columns = columnsTrackConfig,
                Rows = rowsTrackConfig
            };
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TDataType, TContextType>
            ConfigTracks(
                GridMediaSize[] gridMediaSizes,
                string columnsTrackConfig = "",
                string rowsTrackConfig = ""
            )
        {
            if (Element.GridConfig.TrackConfig == null)
            {
                Element.GridConfig.TrackConfig = new Dictionary<string, TrackConfig>();
            }

            foreach (GridMediaSize gridMediaSize in gridMediaSizes)
            {
                Element.GridConfig.TrackConfig[gridMediaSize.ToString().ToLower()] = new TrackConfig
                {
                    Columns = columnsTrackConfig,
                    Rows = rowsTrackConfig
                };
            }            
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
