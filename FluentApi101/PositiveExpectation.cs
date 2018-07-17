using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace FluentApi101
{
    public sealed class PositiveExpectation<T> : PropertyChangedExpectation<T>
        where T : INotifyPropertyChanged
    {
        public PositiveExpectation(T subject, params string[] propertyNames) : base(subject, propertyNames)
        {
        }

        public PositiveExpectation<T> And<TProp>(Expression<Func<T, TProp>> propertyExpression)
        {
            var newPropertyNames =
                ExpectedProps.Concat(new[] { ExpressionUtilities.GetPropertyName(propertyExpression) }).ToArray();

            return new PositiveExpectation<T>(Subject, newPropertyNames);
        }

        public ExclusiveExpectation<T> AndNothingElse()
        {
            return new ExclusiveExpectation<T>(Subject, ExpectedProps);
        }

        public NegativeExpectation<T> ButNot<TProp>(Expression<Func<T, TProp>> propertyExpression)
        {
            return new NegativeExpectation<T>(Subject, ExpectedProps, new [] { ExpressionUtilities.GetPropertyName(propertyExpression) });
        }

    }
}
