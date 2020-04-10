using System.Collections.Generic;
using System.Linq;
using CoreDui.Definitions;
using CoreDui.Enums;
using CoreDui.TaskHandling;

namespace CoreDui.Utils
{
    public static class TaskSearch
    {        
        public static ICollection<TaskDefinition> Search(FlowDefinition flowDefinition, string taskPath, TaskTypeEnum taskType)
        {
            ICollection<TaskDefinition> tasks = null;

            if(flowDefinition.TaskPath == taskPath)
            {
                tasks = flowDefinition.Tasks.Where(x => x.GetTaskType() == taskType).ToList();
                return tasks;
            }
            else if (flowDefinition.Steps != null)
            {
                foreach(var element in flowDefinition.Steps)
                {
                    if (tasks == null)
                    {
                        tasks = RecurseElement(tasks, element, taskPath, taskType);
                    }
                }
            }
            return tasks;
        }

        private static ICollection<TaskDefinition> RecurseElement(ICollection<TaskDefinition> tasks, Element elementIn, string taskPath, TaskTypeEnum taskType)
        {
            if(elementIn.TaskPath == taskPath)
            {                                
                tasks = elementIn.Tasks.Where(x => x.GetTaskType() == taskType).ToList();                
                return tasks;
            }
            else if(elementIn.Elements != null)
            {
                foreach (var element in elementIn.Elements)
                {
                    if (tasks == null)
                    {
                        tasks = RecurseElement(tasks, element, taskPath, taskType);                        
                    }
                }
            }
            return tasks;
        }
        
    }
}
