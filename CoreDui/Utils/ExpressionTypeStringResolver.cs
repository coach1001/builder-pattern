using System.Linq.Expressions;

namespace CoreDui.Utils
{
    public static class ExpressionTypeStringResolver
    {
        public static string Resolve(ExpressionType expressionType)
        {
            switch (expressionType)
            {
                case ExpressionType.Add:
                    return "+";                    
                case ExpressionType.Subtract:
                    return "-";                    
                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    return "&&";
                case ExpressionType.OrElse:
                case ExpressionType.Or:
                    return "||";                    
                case ExpressionType.Equal:
                    return "==";
                case ExpressionType.Not:
                    return "!";
                case ExpressionType.NotEqual:
                    return "!=";                    
                case ExpressionType.LessThan:
                    return "<";                    
                case ExpressionType.LessThanOrEqual:
                    return "<=";                    
                case ExpressionType.GreaterThan:
                    return ">";                    
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                default: return null;
            }            
        }
    }
}
