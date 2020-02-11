using System.Collections.Generic;
using CoreDui.Definitions;
using CoreDui.Enums;

namespace CoreDui.Repositories
{
    public interface IModuleRepository
    {
        void AddModule(ModuleDefinition module);
        void AddFlowToExistingModule(string route, string system, string moduleName, FlowDefinition flow);
        bool Exists(string route, string system, string moduleName);

        ICollection<ModuleDefinition> GetModules();
    }
}
