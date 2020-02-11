using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoreDui.Definitions;

namespace CoreDuiWebApi.Flow
{
    public static class CustomAttribuesJsConverters
    {
        public static ValidatorData MaxLengthJsConverter(object vObject)
        {           
            var attr = (MaxLengthAttribute)vObject;
            var validator = new ValidatorData
            {
                Name = "maxLength",
                Metadata = new Dictionary<string, object>
                    {
                        { "length", attr.Length }
                    }
            };
            return validator;
        }
    }
}
