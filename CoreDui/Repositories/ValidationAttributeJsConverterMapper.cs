using System;
using System.Collections.Generic;
using CoreDui.Definitions;
using System.Linq;
using CoreDui.Attributes;

namespace CoreDui.Repositories
{
    public class ValidationAttributeJsConverterMapper : IValidationAttributeJsConverterMapper
    {
        public Dictionary<string, Func<object, ValidatorData>> Repository { get; set; } = new Dictionary<string, Func<object, ValidatorData>>();

        public ValidationAttributeJsConverterMapper()
        {           
            AddValidator("RequiredAttribute", AttributesJsConverters.RequiredJsConverter);
            AddValidator("MustMatchAttribute", AttributesJsConverters.MustMatchJsConverter);
            AddValidator("RequiredIfAttribute", AttributesJsConverters.RequiredIfJsConverter);
            AddValidator("MinLengthAttribute", AttributesJsConverters.MinLengthJsConverter);
            AddValidator("EmailAddressAttribute", AttributesJsConverters.EmailAddressJsConverter);
            AddValidator("RangeAttribute", AttributesJsConverters.RangeJsConverter);
        }

        public void AddValidator(string name, Func<object, ValidatorData> function)
        {
            Repository.Add(name, function);
        }

        public Func<object, ValidatorData> GetValidator(string name)
        {
            return Repository.FirstOrDefault(validator => validator.Key == name).Value;
        }
    }
}
