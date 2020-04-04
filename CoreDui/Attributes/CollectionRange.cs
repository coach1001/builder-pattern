using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CoreDui.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class CollectionRangeAttribute : ValidationAttribute
    {        
        public readonly int MaxCount;
        public readonly int MinCount;
        public readonly bool ParentScope = true;

        public CollectionRangeAttribute(int minCount, int maxCount)
        {
            MinCount = minCount;
            MaxCount = maxCount;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var objectCollection = value as ICollection;

            if(objectCollection.Count == 0)
            {
                return ValidationResult.Success;
            } else if(objectCollection.Count > MaxCount)
            {
                return new ValidationResult($"Less than {MaxCount} rows required");
            } 
            else if(objectCollection.Count < MinCount)
            {
                return new ValidationResult($"Greater than {MinCount} rows required");
            }
            return ValidationResult.Success;
         }
    }
}
