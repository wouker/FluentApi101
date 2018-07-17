using System;
using FluentApi101;
using NUnit.Framework;

namespace UnitTestProject_FluentApi101
{
    [TestFixture]
    public class ExclusiveTests
    {
        [Test]
        public void SingleProperty_AssertFail()
        {
            var person = new Person();

            var ex = Assert.Throws<AssertionException>(() => person.ShouldNotifyFor(x => x.FirstName)
                .AndNothingElse()
                .When(() => person.FirstName = "John"));

            var expectedMessage =
                "Received notifications: FirstName, FullName. Expected: FirstName and nothing else.";

            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void SingleProperty_AssertPass()
        {
            var person = new Person();

            person.ShouldNotifyFor(x => x.BirthDate)
                .AndNothingElse()
                .When(() => person.BirthDate = new DateTime(2013, 9, 7));
        }

        [Test]
        public void MultipleProperties_AssertPass()
        {
            var person = new Person();

            person.ShouldNotifyFor(x => x.FirstName)
                .And(x => x.FullName)
                .AndNothingElse()
                .When(() => person.FirstName = "John");
        }
    }
}
