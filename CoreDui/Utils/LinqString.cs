using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using CoreDui.Extensions;

namespace CoreDui.Utils
{
    public class LinqJsString : ExpressionVisitor
    {
        public StringBuilder sb = new StringBuilder();

        protected override Expression VisitBinary(BinaryExpression b)
        {
            sb.Append("(");
            this.Visit(b.Left);
            ExpressionType t = b.NodeType;

            sb.Append(ExpressionTypeStringResolver.Resolve(b.NodeType));

            this.Visit(b.Right);
            sb.Append(")");
            return b;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {

            var result = Expression.Lambda(m).Compile().DynamicInvoke();
            var type = result.GetType().Name;
            
            switch(type)
            {
                case "DateTime":
                    var dateTime = (DateTime)result;
                    var utcTime = (DateTimeOffset) DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                    sb.Append(utcTime.ToUnixTimeMilliseconds());
                    break;
                case "TimeSpan":
                    var timeSpan = (TimeSpan)result;
                    sb.Append(timeSpan.TotalMilliseconds);
                    break;
            }

            //string s = m.ToString();
            //sb.Append("\"").Append(result.ToString()).Append("\"");  
            //string prop = m.Arguments[0].ToString();
            //sb.Append(prop.Substring(prop.IndexOf(".") + 1) + "." + m.Method.Name + "(");
            //Visit(m.Arguments[1]);

            return m;
        }

        Stack<object> _stack = new Stack<object>();
        protected override Expression VisitMember(MemberExpression m)
        {
            var e = base.VisitMember(m);
            var c = m.Expression as ConstantExpression;
            if (c != null)
            {
                Type t = c.Value.GetType();
                var x = t.InvokeMember(m.Member.Name, BindingFlags.GetField |
                    BindingFlags.GetProperty |
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.Static, null, c.Value, null);

                if (x is string)
                    sb.Append("\"").Append(x).Append("\"");
                else if (!x.GetType().IsClass)
                {   // check for complex structs
                    if (x is DateTime || x is Guid)
                        sb.Append("\"").Append(x).Append("\"");
                    else // numbers
                        sb.Append(x);
                }
                else if (x.GetType().IsArray)
                {
                    var a = x as IList;
                    for (int i = 0; i < a.Count - 1; i++)
                    {
                        sb.Append(a[i]);
                        sb.Append(",");
                    }
                    sb.Append(a[a.Count - 1]);
                }
                else
                    _stack.Push(x);
            }

            if (m.Expression != null)
            {                
                if (m.Expression.NodeType == ExpressionType.Parameter) // property
                {
                    sb.Append(m.Member.Name.FirstCharToLower());
                }
                else if (m.Expression.NodeType == ExpressionType.MemberAccess) // obj.property
                {
                    if(m.Member.Name == "Count")
                    {
                        sb.Append(".length");
                    }
                    else
                    {
                        Type t = m.Expression.Type;
                        var f = m.Expression as MemberExpression;
                        var val = t.InvokeMember(m.Member.Name, BindingFlags.GetField |
                            BindingFlags.GetProperty |
                            BindingFlags.Public |
                            BindingFlags.NonPublic |
                            BindingFlags.Instance |
                            BindingFlags.Static, null, _stack.Pop(), null);

                        if (val is string)
                            sb.Append("\"").Append(val).Append("\"");
                        else if (!val.GetType().IsClass)
                        {   // check for complex structs
                            if (val is DateTime || val is Guid)
                                sb.Append("\"").Append(val).Append("\"");
                            else // numbers
                                sb.Append(val);
                        }
                        else
                            _stack.Push(val);
                    }
                }
            }
            return e;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            IQueryable q = c.Value as IQueryable;
            if (q != null)
            {
                sb.Append(q.ElementType.Name);
            }
            else if (c.Value == null)
            {
                sb.Append("NULL");
            }
            else
            {
                //_stack.Push(c.Value);
                //if (Type.GetTypeCode(c.Value.GetType()) == TypeCode.Object)
                //    _stack.Pop();                
                switch (Type.GetTypeCode(c.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        //sb.Append(((bool)c.Value) ? 1 : 0);
                        sb.Append(((bool)c.Value) ? "true" : "false");
                        break;
                    case TypeCode.String:
                        sb.Append("\"");
                        sb.Append(c.Value);
                        sb.Append("\"");
                        break;
                    case TypeCode.Object:
                        break;
                    default:
                        sb.Append(c.Value);
                        if (--_count > 0)
                            sb.Append(",");
                        break;
                }
            }
            return c;
        }

        int _count = 0;
        public override Expression Visit(Expression node)
        {
            if (node.NodeType == ExpressionType.NewArrayInit)
            {
                var a = node as NewArrayExpression;
                _count = a.Expressions.Count;
                return base.Visit(node);
            }
            else
            {
                return base.Visit(node);
            }
                
        }
    }

}
