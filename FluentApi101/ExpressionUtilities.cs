using System;
using System.Linq.Expressions;

namespace FluentApi101
{
    internal static class ExpressionUtilities
    {
        public static string GetPropertyName<T, TProp>(Expression<Func<T, TProp>> propertyExpression)
        {
            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
                throw new ArgumentException("Expression must be of the form \"x => x.PropertyName\".");

            return body.Member.Name;
        }
    }
}
