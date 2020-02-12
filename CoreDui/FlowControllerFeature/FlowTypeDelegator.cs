using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CoreDui.Definitions;

namespace CoreDui.FlowControllerFeature
{
    public class FlowTypeDelegator<TAttribute> : TypeDelegator where TAttribute : FlowDelegationType
    {
        private readonly TAttribute _attribute;
        private readonly TypeInfo _delegatingType;

        public FlowTypeDelegator(TypeInfo delegatingType, TAttribute attribute)
            
            : base(delegatingType)
        {
            _delegatingType = delegatingType;
            _attribute = attribute;
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            if (attributeType == typeof(TAttribute))
            {
                return new[] { _attribute };
            }

            return base.GetCustomAttributes(attributeType, inherit);
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            var attributes = base.GetCustomAttributes(inherit);

            var result = new object[attributes.Length + 1];
            attributes.CopyTo(result, 0);
            result[attributes.Length] = _attribute;
            return result;
        }

        public override Type GetGenericTypeDefinition()
        {
            return _delegatingType.GetGenericTypeDefinition();
        }

        public override Type AsType()
        {
            return _delegatingType.AsType();
        }
    }
}
