using System;
using CoreDui.Definitions;

namespace CoreDui.Repositories
{
    public interface IValidationAttributeJsConverterMapper
    {
        void AddValidator(string name, Func<object, ValidatorData> function);

        Func<object, ValidatorData> GetValidator(string name);
    }
}
