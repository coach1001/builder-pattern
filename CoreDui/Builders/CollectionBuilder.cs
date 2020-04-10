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
using System.Reflection;
using System.Linq;

namespace CoreDui.Builders
{
    public class CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>
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

        public CollectionBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>, TDataType, TDerived, TContextType>
            AddGroup<TDerived>(Expression<Func<TDataType, TDerived>> property, string name = null)
        {
            var expression = (MemberExpression)property.Body;
            var attrs = expression.Member.GetCustomAttributes(false);
            var validators = GenerateValidationModel.Generate(attrs, _validationMapper);
            var modelProperty = expression.Member.Name.FirstCharToLower();
            // name = name != null ? name : modelProperty;

            var builder = new CollectionBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>, TDataType, TDerived, TContextType>
                (this, name, _elementMapper, _controlMapper, _validationMapper);

            builder.Element.ModelProperty = modelProperty;
            builder.Element.TaskPath = $"{Element.TaskPath}.{modelProperty}";
            builder.Element.DataType = property.ReturnType;
            builder.Element.UiTemplate = _elementMapper.GetDefault(ElementType.Object);
            builder.Element.Tasks = new List<TaskDefinition>();

            if (validators != null)
            {
                builder.Element.Validators = validators;
            }
            Element.Elements.Add(builder.Element);
            return builder;
        }

        public CollectionBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>, TDataType, TDerived, TContextType>
            AddArray<TDerived>(Expression<Func<TDataType, ICollection<TDerived>>> property, string name = null)
        {
            var expression = (MemberExpression)property.Body;
            var attrs = expression.Member.GetCustomAttributes(false);
            var validators = GenerateValidationModel.Generate(attrs, _validationMapper);
            var modelProperty = expression.Member.Name.FirstCharToLower();
            // name = name != null ? name : modelProperty;

            var builder = new CollectionBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>, TDataType, TDerived, TContextType>(
                this, name, _elementMapper, _controlMapper, _validationMapper, ElementType.Array);

            builder.Element.ModelProperty = modelProperty;
            builder.Element.TaskPath = $"{Element.TaskPath}.{modelProperty}[]";
            builder.Element.DataType = property.ReturnType.GetGenericArguments()[0];
            builder.Element.UiTemplate = _elementMapper.GetDefault(ElementType.Array);
            builder.Element.Tasks = new List<TaskDefinition>();

            var collectionHasId = typeof(TDerived).GetProperties().Any(m => m.Name == "Id__");

            if(collectionHasId)
            {
                builder.Element.Elements.Add(new Element
                {
                    ModelProperty = "id__",
                    TaskPath = $"{Element.TaskPath}.{modelProperty}[].id__",
                    ElementType = ElementType.Control,
                    DataType = typeof(Guid),   
                    Tasks = new List<TaskDefinition>()
                });
                builder.Element.Elements.Add(new Element
                {
                    ModelProperty = "operation__",
                    TaskPath = $"{Element.TaskPath}.{modelProperty}[].operation__",
                    ElementType = ElementType.Control,
                    DataType = typeof(ArrayItemOperation),
                    Tasks = new List<TaskDefinition>()
                });
            }

            if (validators != null)
            {
                builder.Element.Validators = validators;
            }
            Element.Elements.Add(builder.Element);
            return builder;
        }

        public ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>, TDataType, TContextType>
            AddControl<TPropertyType>(Expression<Func<TDataType, TPropertyType>> property, ControlType controlType = ControlType.Text, string name = null)
        {
            var expression = (MemberExpression)property.Body;
            var attrs = expression.Member.GetCustomAttributes(false);
            var validators = GenerateValidationModel.Generate(attrs, _validationMapper);
            var modelProperty = expression.Member.Name.FirstCharToLower();
            // name = name != null ? name : modelProperty;

            var builder = new ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>, TDataType, TContextType>(
                this, name, _controlMapper, controlType);

            builder.Element.ModelProperty = modelProperty;
            builder.Element.TaskPath = $"{Element.TaskPath}.{modelProperty}";
            builder.Element.DataType = property.ReturnType;
            builder.Element.UiTemplate = _controlMapper.GetDefault(controlType);
            builder.Element.Tasks = new List<TaskDefinition>();

            if (validators != null)
            {
                builder.Element.Validators = validators;
            }
            Element.Elements.Add(builder.Element);
            return builder;
        }


        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>
            GridConfig(string column, string row = "", GridMediaSize mediaSize = GridMediaSize.Large)
        {
            if(mediaSize == GridMediaSize.Large)
            {
                Element.GridConfig = new GridConfig();
                Element.GridConfig.Large = new MediaConfig
                {
                    MediaSize = GridMediaSize.Large,
                    Column = column,
                    Row = row
                };
            }
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>
            PositionConfig(string column, string row = "", GridMediaSize mediaSize = GridMediaSize.Large)
        {
            if (mediaSize == GridMediaSize.Large)
            {
                Element.PositionConfig = new PositionConfig();
                Element.PositionConfig.Large = new MediaConfig
                {
                    MediaSize = GridMediaSize.Large,
                    Column = column,
                    Row = row
                };
            }
            return this;
        }

        public ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>, TDataType, TContextType>
            AddDecorator(string name, ControlType controlType = ControlType.Decorator)
        {
            
            var builder = new ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>, TDataType, TContextType>(
                this, name, _controlMapper, controlType);
            builder.Element.ModelProperty = null;
            builder.Element.TaskPath = this.Element.TaskPath;
            builder.Element.UiTemplate = _controlMapper.GetDefault(controlType);
            Element.Elements.Add(builder.Element);
            return builder;
        }

        public ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>, TDataType, TContextType>
            AddSpacer(ControlType controlType = ControlType.Spacer)
        {
            var builder = new ControlBuilder<TFlowDataType, CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>, TDataType, TContextType>(
                this, null, _controlMapper, controlType);
            builder.Element.ModelProperty = ShortId.Generate(false, false, 14);
            builder.Element.UiTemplate = _controlMapper.GetDefault(controlType);            
            Element.Elements.Add(builder.Element);
            return builder;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>
            WithReactivity<TCustomType>(Expression<Func<TCustomType, bool>> ex, ReactivityType reactivityType)
        {
            var linqString = new LinqJsString();
            linqString.Visit(ex);
            var output = linqString.sb.ToString();
            if (Element.Reactivity == null)
            {
                Element.Reactivity = new List<ReactivityExpression>();
            }
            Element.Reactivity.Add(new ReactivityExpression
            {
                Expression = output,
                Type = reactivityType
            });
            return this;            
        }

        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>
            WithReactivity(Expression<Func<TParentDataType, bool>> ex, ReactivityType reactivityType)
        {
            var linqString = new LinqJsString();
            linqString.Visit(ex);
            var output = linqString.sb.ToString();
            if(Element.Reactivity == null)
            {
                Element.Reactivity = new List<ReactivityExpression>();
            }
            Element.Reactivity.Add(new ReactivityExpression
            {
                Expression = output,
                Type = reactivityType
            });
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType> WithBorder(BorderEnum borderConfig)
        {
            Element.BorderConfig = borderConfig;
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType> Back(string text)
        {
            Element.BackButton = text;
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType> Next(string text)
        {
            Element.NextButton = text;
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType> Vertical(int rows = 0)
        {
            Element.Vertical = true;
            Element.VerticalRows = rows;
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType> MaxRows(int rows = 0)
        {
            Element.MaxRows = rows;            
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>
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
                RequiresValidDataToExecute = false,
                ExecuteOn = ExecuteOn.Api
            };
            Element.Tasks.Add(task);
            return this;
        }

        public CollectionBuilder<TFlowDataType, TParentType, TParentDataType, TDataType, TContextType>
            WithUiTask(TaskTypeEnum type, string uiTask)
        {
            if (Element.Tasks == null)
            {
                Element.Tasks = new List<TaskDefinition>();
            }
            var task = new TaskDefinition
            {
                Type = null,
                TaskType = type,    
                ExecuteOn = ExecuteOn.Ui,
                RequiresValidDataToExecute = false,
                UiTask = uiTask
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
