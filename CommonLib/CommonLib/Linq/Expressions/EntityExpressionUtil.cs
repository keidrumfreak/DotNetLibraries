using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CommonLib.Linq.Expressions.Entity
{
    public static class EntityExpressionUtil
    {
        public static Expression Like(this MemberExpression member, string value, bool withNull = false)
        {
            var expr = member.Contains(value);
            if (!withNull)
                return expr;
            return expr.OrElse(member.IsNull());
        }

        public static Expression NotLike(this MemberExpression member, string value, bool excludeNull = false)
        {
            var expr = Expression.Not(member.Contains(value));
            if (excludeNull)
                return expr;
            return expr.OrElse(member.IsNull());
        }

        public static Expression Exists<T>(this MemberExpression member, Expression<Func<T, bool>> expression)
        {
            var any = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
            .First(m => m.Name == "Any" && m.GetParameters().Count() == 2);

            return Expression.Call(any.MakeGenericMethod(typeof(T)), member, expression);
        }
    }
}
