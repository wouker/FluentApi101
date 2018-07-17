using System;
using FluentApi101;
using NUnit.Framework;

namespace UnitTestProject_FluentApi101
{
    [TestFixture]
    public class ArgumentValidationTests
    {
        [Test]
        public void NonPropertyExpression_Exception()
        {
            var person = new Person();

            var ex = Assert.Throws<ArgumentException>(() =>
                person.ShouldNotifyFor(x => "not a property expression")
            );

            Assert.That(ex.Message, Is.EqualTo("Expression must be of the form \"x => x.PropertyName\"."));
        }

        [Test]
        public void PositiveNegative_Conflict()
        {
            var person = new Person();

            var ex = Assert.Throws<ArgumentException>(() =>
                person.ShouldNotifyFor(x => x.FirstName)
                    .ButNot(x => x.FirstName));

            Assert.That(ex.Message,
                Is.EqualTo(
                    "Cannot specify properties for both positive and negative verification. Conflicting properties: FirstName"));
        }
    }
}
