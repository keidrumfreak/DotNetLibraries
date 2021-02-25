using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;

namespace CommonLib.Linq.Expressions
{
    public static class ExpressionUtil
    {
        public static Expression<T> ToLambda<T>(this Expression expression, ParameterExpression param)
        {
            return Expression.Lambda<T>(expression, param);
        }

        public static Expression AndAlso(this Expression left, Expression right)
        {
            if (left == default)
                return right;
            return Expression.AndAlso(left, right);
        }

        public static Expression OrElse(this Expression left, Expression right)
        {
            if (left == default)
                return right;
            return Expression.OrElse(left, right);
        }

        public static BinaryExpression Equal<T>(this Expression member, T value)
        {
            return Expression.Equal(member, Expression.Constant(value, typeof(T)));
        }

        public static BinaryExpression NotEqual<T>(this Expression member, T value)
        {
            return Expression.NotEqual(member, Expression.Constant(value, typeof(T)));
        }

        public static BinaryExpression GreaterThanOrEqual<T>(this Expression member, T value)
        {
            return Expression.GreaterThanOrEqual(member, Expression.Constant(value, typeof(T)));
        }

        public static BinaryExpression LessThanOrEqual<T>(this Expression member, T value)
        {
            return Expression.LessThanOrEqual(member, Expression.Constant(value, typeof(T)));
        }

        public static BinaryExpression IsNull(this Expression member)
        {
            return Expression.Equal(member, Expression.Constant(null, typeof(object)));
        }

        public static Expression Contains(this MemberExpression member, string value)
        {
            return Expression.Call(member, typeof(string).GetMethod("Contains"), Expression.Constant(value));
        }

        public static MemberExpression Member(this Expression parameter, string property)
        {
            return Expression.PropertyOrField(parameter, property);
        }
    }
}
