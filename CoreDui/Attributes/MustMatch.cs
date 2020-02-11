using System;
using System.ComponentModel.DataAnnotations;

namespace CoreDui.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class MustMatchAttribute : ValidationAttribute
    {        
        public readonly string TargetField;                
        public readonly bool ParentScope = true;

        public MustMatchAttribute(string targetField)
        {            
            TargetField = targetField;     
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            var instance = validationContext.ObjectInstance;
            Type type = instance.GetType();
            var targetFieldValue = type.GetProperty(TargetField).GetValue(instance, null);
            if (value != null && targetFieldValue != null && value.ToString() == targetFieldValue.ToString())
            {
                return ValidationResult.Success;    
            }
            else 
            {
                return new ValidationResult($"Does not match {TargetField}");
            }

        }
    }
}
