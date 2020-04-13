using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CoreDui.Definitions;
using CoreDui.Enums;

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
            ICollection collection = value as ICollection;
            var objectList = new List<BaseCollectionModel>();

            if (collection != null)
            {
                foreach (BaseCollectionModel item in collection)
                {
                    objectList.Add(item);
                }
            }
            
            var actualCount = objectList.Count(m => m.Operation__ != ArrayOperation.Remove);

            if(objectList.Count > MaxCount)
            {
                return new ValidationResult($"Less than {MaxCount} rows required");
            } 
            else if(objectList.Count < MinCount)
            {
                return new ValidationResult($"Greater than {MinCount} rows required");
            }
            
            return ValidationResult.Success;
         }
    }
}
