using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;

namespace FluentApi101
{
    //constraint context
    public sealed class ExclusiveExpectation<T> : PropertyChangedExpectation<T>
        where T : INotifyPropertyChanged
    {

        public ExclusiveExpectation(T subject, IEnumerable<string> expectedProperties) : base(subject, expectedProperties)
        {
        }

        public override void When(Action action)
        {
            var notifications = new List<string>();
            Subject.PropertyChanged += (o, e) => notifications.Add(e.PropertyName);

            action();

            var unexpected = notifications.Except(ExpectedProps);
            if(!unexpected.Any())
                return;

            var receivedText = notifications.Any() ? FormatNames(notifications) : "(none)";

            var message = $"Received notifications: {receivedText}. " +
                          $"Expected: {FormatNames(ExpectedProps)} and nothing else.";

            Assert.Fail(message);
        }
    }
}
