using System.Collections.Generic;
using System.Linq;
using CoreDui.Enums;

namespace CoreDui.Repositories
{
    public class ElementTypeTemplateMapper : IElementTypeTemplateMapper
    {
        public Dictionary<string, ElementTypeTemplate> Repository { get; set; }

        public ElementTypeTemplateMapper()
        {
            Repository = new Dictionary<string, ElementTypeTemplate>();

            AddTemplate("defaultObject", ElementType.Object, true);
            AddTemplate("defaultArray", ElementType.Array, true);
            AddTemplate("defaultControl", ElementType.Array, true);
        }

        public void AddTemplate(string templateName, ElementType elementType, bool default_ = false)
        {
            Repository.Add(templateName, new ElementTypeTemplate
            {
                Type = elementType,
                Default = default_
            });
        }

        public string GetDefault(ElementType elementType)
        {
            return Repository.FirstOrDefault(template => template.Value.Type == elementType && template.Value.Default).Key;
        }
    }

    public class ElementTypeTemplate
    {
        public ElementType Type { get; set; }
        public bool Default { get; set; } = false;

    }
}
