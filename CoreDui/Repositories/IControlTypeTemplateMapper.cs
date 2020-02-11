using CoreDui.Enums;

namespace CoreDui.Repositories
{
    public interface IControlTypeTemplateMapper
    {
        void AddTemplate(string templateName, ControlType controlType, bool default_ = false);
        string GetDefault(ControlType controlType);

        ControlTypeTemplate CheckForTemplate(ControlType controlType, string uiTemplate);
    }
}
