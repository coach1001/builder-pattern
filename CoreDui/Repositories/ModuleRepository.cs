using System;
using System.Collections.Generic;
using System.Text;
using CoreDui.Definitions;
using CoreDui.Enums;
using System.Linq;

namespace CoreDui.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        public ICollection<ModuleDefinition> Modules { get; set; }

        public ModuleRepository()
        {
            Modules = new List<ModuleDefinition>();
        }

        public void AddModule(ModuleDefinition module)
        {
            Modules.Add(module);
        }

        public void AddFlowToExistingModule(string route, string system, string moduleName, FlowDefinition flow)
        {
            foreach(ModuleDefinition m in Modules)
            {
                if(m.Route == route && m.System == system && m.Module == moduleName)
                {
                    m.Flows.Add(flow);
                }
            }
        }

        public bool Exists(string route, string system, string moduleName)
        {
            var moduleExists = false;
            foreach(var module in Modules)
            {
                if(!moduleExists)
                {
                    moduleExists = module.Route == route && module.System == system && module.Module == moduleName;
                }
            }
            return moduleExists;
        }

        public ICollection<ModuleDefinition> GetModules()
        {
            return Modules;
        }
    }
}
