using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CoreDui.Definitions;
using CoreDui.Enums;
using CoreDui.Repositories;
using CoreDui.TaskHandling;
using CoreDui.Utils;

namespace CoreDui.Builders
{
    public class ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
    {
        public TParentType Parent { get; set; }
        public Element Element { get; set; }
        
        private readonly IControlTypeTemplateMapper _controlMapper;

        public ControlBuilder(
            TParentType parent, string name, IControlTypeTemplateMapper controlMapper, ControlType controlType = ControlType.Text, ElementType elementType = ElementType.Control)
        {
            Parent = parent;
            _controlMapper = controlMapper;
            Element = new Element
            {
                Name = name,
                ElementType = elementType,
                ControlType = controlType,                
            };
        }

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType> SetControlType(ControlType controlType, string uiTemplate = null)
        {
            Element.ControlType = controlType;
            if(string.IsNullOrEmpty(uiTemplate))
            {
                Element.UiTemplate = _controlMapper.GetDefault(controlType);
            }
            else
            {
                _controlMapper.CheckForTemplate(controlType, uiTemplate);
                Element.UiTemplate = uiTemplate;
            }
            return this;
        }

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
            WithMetadata(string key, object value)            
        {
            if (Element.Metadata == null)
            {
                Element.Metadata = new Dictionary<string, object>();
            }
            Element.Metadata[key] = value;
            return this;
        }

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
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

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
            WithReactivity(Expression<Func<TParentDataType, bool>> ex, ReactivityType reactivityType)
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

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
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

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
            WithPrefix(string value, bool innerHtml = false)
        {
            if (Element.Prefix == null)
            {
                Element.Prefix = new AppendInput();
            }
            Element.Prefix.Value = value;
            Element.Prefix.InnerHtml = innerHtml;
            return this;
        }

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
            WithSuffix(string value, bool innerHtml = false)
        {
            if (Element.Suffix == null)
            {
                Element.Suffix = new AppendInput();
            }
            Element.Suffix.Value = value;
            Element.Suffix.InnerHtml = innerHtml;
            return this;
        }

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
            InitiallyDisabled(bool disabled = true)
        {
            Element.InitiallyDisabled = disabled;
            return this;
        }

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
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


        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType> UiTemplate(string uiTemplate)
        {
            Element.UiTemplate = uiTemplate;
            return this;
        }

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType> WithBorder(BorderEnum borderConfig)
        {
            Element.BorderConfig = borderConfig;
            return this;
        }

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
            WithOptions(ICollection<SelectOption> options)
        {
            Element.Options = new List<SelectOption>(options);            
            return this;
        }

        public ControlBuilder<TFlowDataType, TParentType, TParentDataType, TContextType>
            WithDefaultValue(object value)
        {
            Element.DefaultValue = value;
            return this;
        }

        public TParentType End()
        {
            return Parent;
        }
    }

}
