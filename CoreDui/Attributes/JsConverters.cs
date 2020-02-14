using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CoreDui.Definitions;
using CoreDui.Extensions;

namespace CoreDui.Attributes
{
    public static class AttributesJsConverters
    {
        public static ValidatorData RequiredJsConverter(object vObject)
        {
            var attr = (RequiredAttribute)vObject;
            var validator = new ValidatorData
            {
                Name = "required",
                Metadata = new Dictionary<string, object>()
            };
            return validator;
        }

        public static ValidatorData MustMatchJsConverter(object vObject)
        {
            var attr = (MustMatchAttribute)vObject;
            var validator = new ValidatorData
            {
                Name = "mustMatch",
                ParentScope = attr.ParentScope,
                Metadata = new Dictionary<string, object>
                        {
                            { "targetField", attr.TargetField.FirstCharToLower() }
                        }
            };
            return validator;
        }

        public static ValidatorData RequiredIfJsConverter(object vObject)
        {
            var attr = (RequiredIfAttribute)vObject;
            var validator = new ValidatorData
            {
                Name = "requiredIf",
                ParentScope = attr.ParentScope,
                Metadata = new Dictionary<string, object>
                        {
                            { "triggerField", attr.TriggerField.FirstCharToLower() },
                            { "triggerValue", attr.TriggerValue.ToString() }                            
                        }
            };
            return validator;
        }

        public static ValidatorData MinLengthJsConverter(object vObject)
        {
            var attr = (MinLengthAttribute)vObject;
            var validator = new ValidatorData
            {
                Name = "minLength",
                Metadata = new Dictionary<string, object>
                    {
                        { "length", attr.Length }
                    }
            };
            return validator;
        }

        public static ValidatorData EmailAddressJsConverter(object vObject)
        {
            var attr = (EmailAddressAttribute)vObject;
            var validator = new ValidatorData
            {
                Name = "emailAddress",
                Metadata = new Dictionary<string, object>()
            };
            return validator;
        }
    }
}
