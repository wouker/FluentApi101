using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FluentApi101
{
    public static class NotifyPropertyChangedExtensions
    {
        public static PositiveExpectation<T> ShouldNotifyFor<T, TProp>(
            this T subject,
            Expression<Func<T, TProp>> propertyExpression)
            where T : INotifyPropertyChanged
        {
            return new PositiveExpectation<T>(subject, ExpressionUtilities.GetPropertyName(propertyExpression));
        }

    }
}
