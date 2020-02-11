using CoreDui.Enums;

namespace CoreDui.Repositories
{
    public interface IElementTypeTemplateMapper
    {
        void AddTemplate(string templateName, ElementType elementType, bool default_ = false);
        string GetDefault(ElementType elementType);
        
    }
}
