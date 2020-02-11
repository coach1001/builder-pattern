using System;
using System.Collections.Generic;
using CoreDui.Definitions;
using CoreDui.Repositories;

namespace CoreDui.Utils
{
    public static class GenerateValidationModel
    {
        public static ICollection<ValidatorData> Generate(ICollection<object> attrs, IValidationAttributeJsConverterMapper validatorMapper)
        {
            var validators = new List<ValidatorData>();            
            foreach (var attr_ in attrs)
            {
                var attr__ = (Attribute) attr_;
                var attrName = attr__.GetType().Name;
                var validator = validatorMapper.GetValidator(attrName).Invoke(attr_);
                if(validator != null)
                {
                    validators.Add(validator);
                }
            }
            return validators.Count > 0 ? validators : null;
        }
    }

}
