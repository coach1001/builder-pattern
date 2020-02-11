using System;
using System.ComponentModel.DataAnnotations;

namespace CoreDui.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class RequiredIfAttribute : RequiredAttribute
    {        
        public readonly string TriggerField;
        public readonly object TriggerValue;        
        public readonly bool ParentScope = true;

        public RequiredIfAttribute(string triggerField, object triggerValue)
        {            
            TriggerField = triggerField;
            TriggerValue = triggerValue;                        
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            var instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            var triggerValue = type.GetProperty(TriggerField).GetValue(instance, null);

            if (TriggerValue.ToString() == triggerValue.ToString())
            {
                ValidationResult result = base.IsValid(value, validationContext);
                return result;
            }
            return ValidationResult.Success;

        }
    }
}
