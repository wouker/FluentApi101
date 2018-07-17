using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace FluentApi101
{
    public sealed class NegativeExpectation<T> : PropertyChangedExpectation<T>
        where T : INotifyPropertyChanged
    {
        public NegativeExpectation(T subject, IEnumerable<string> expected, IEnumerable<string> notExpected) : base(subject, expected, notExpected)
        {
        }

        public NegativeExpectation<T> Nor<TProp>(Expression<Func<T, TProp>> propertyExpression)
        {
            var newUnexpected =
                NotExpected.Concat(new[] { ExpressionUtilities.GetPropertyName(propertyExpression) }).ToArray();
            
            return new NegativeExpectation<T>(Subject, ExpectedProps, newUnexpected);
        }
    }
}
