using System.Collections.Generic;
using System.Linq;
using CoreDui.Enums;

namespace CoreDui.Repositories
{
    public class ControlTypeTemplateMapper : IControlTypeTemplateMapper
    {
        public Dictionary<string, ControlTypeTemplate> Repository { get; set; }

        public ControlTypeTemplateMapper()
        {
            Repository = new Dictionary<string, ControlTypeTemplate>();

            AddTemplate("defaultText", ControlType.Text, true);
            AddTemplate("defaultBoolean", ControlType.Boolean, true);
            AddTemplate("defaultNumber", ControlType.Number, true);
            AddTemplate("defaultSelect", ControlType.Select, true);
            AddTemplate("defaultMultiselect", ControlType.MultiSelect, true);
            AddTemplate("defaultDatetime", ControlType.DateTime, true);
            AddTemplate("defaultHideableText", ControlType.HideableText, true);
        }

        public void AddTemplate(string templateName, ControlType controlType, bool default_ = false)
        {
            Repository.Add(templateName, new ControlTypeTemplate
            {
                ControlType = controlType,
                Default = default_
            });
        }

        public string GetDefault(ControlType controlType)
        {
            return Repository.FirstOrDefault(template => template.Value.ControlType == controlType && template.Value.Default).Key;
        }

        public ControlTypeTemplate CheckForTemplate(ControlType controlType, string uiTemplate)
        {
            return Repository.Single(template => template.Key == uiTemplate && template.Value.ControlType == controlType).Value;
        }
    }

    public class ControlTypeTemplate
    {
        public ControlType ControlType { get; set; }
        public bool Default { get; set; } = false;

    }
}
