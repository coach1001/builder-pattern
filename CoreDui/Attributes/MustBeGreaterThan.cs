using System;
using System.ComponentModel.DataAnnotations;

namespace CoreDui.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class MustBeGreaterThanAttribute : ValidationAttribute
    {        
        public readonly string TargetField;                
        public readonly bool ParentScope = true;

        public MustBeGreaterThanAttribute(string targetField)
        {            
            TargetField = targetField;     
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            var instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            var targetFieldValue = type.GetProperty(TargetField).GetValue(instance, null);
            float.TryParse(value.ToString(), out var valueFloat);
            float.TryParse(value.ToString(), out var targetFloat);
            if (value != null && targetFieldValue != null && valueFloat > targetFloat)
            {                
                return ValidationResult.Success;    
            }
            else 
            {
                return new ValidationResult($"Cannot be less than {TargetField}");
            }

        }
    }
}
