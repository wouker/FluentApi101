using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;

namespace FluentApi101
{
    public abstract class PropertyChangedExpectation<T>
        where T : INotifyPropertyChanged
    {
        protected readonly T Subject;
        protected readonly IEnumerable<string> ExpectedProps;
        protected readonly IEnumerable<string> NotExpected;

        protected PropertyChangedExpectation(T subject, IEnumerable<string> expected)
        : this(subject, expected, new string[0])
        {
        }

        protected PropertyChangedExpectation(T subject, IEnumerable<string> expected, IEnumerable<string> notExpected)
        {
            Subject = subject;
            ExpectedProps = expected;
            NotExpected = notExpected;

            var conflicts = ExpectedProps.Intersect(NotExpected).ToList();
            if (conflicts.Any())
                throw new ArgumentException("Cannot specify properties for both positive and negative verification. " +
                                            $"Conflicting properties: {FormatNames(conflicts)}");
        }

        public virtual void When(Action action)
        {
            var notifications = new List<string>();
            Subject.PropertyChanged += (o, e) => notifications.Add(e.PropertyName);

            action();

            var unmetExpectations = ExpectedProps.Except(notifications).ToList();
            var unexpectedNotifications = NotExpected.Intersect(notifications).ToList();

            if (!unmetExpectations.Any() && !unexpectedNotifications.Any())
                return; //no unmet expectations and no unexpected: all ok

            var receivedText = notifications.Any() ? FormatNames(notifications) : "(none)";

            var message = $"Received notifications: {receivedText}. " +
                             $"Expected: {FormatNames(ExpectedProps)}";

            if (NotExpected.Any())
                message += $" but not { FormatNames(NotExpected)}.";
            else
                message += ".";

            Assert.Fail(message);
        }

        protected string FormatNames(IEnumerable<string> propertyNames)
        {
            return string.Join(", ", propertyNames.Select(x => $"{x}"));
        }
    }
}