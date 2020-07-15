using System;
using Codibly.Services.Mailer.Domain.Model;
using NUnit.Framework;

namespace Codibly.Services.Mailer.Tests
{
    public class EmailAddressTests
    {
        [Test]
        public void GivenCorrectEmailAddress_ShouldCreateInstance()
        {
            const string emailAddress = "test@test.com";

            var instance = EmailAddress.Create(emailAddress);

            Assert.That(instance, Is.Not.Null);
            Assert.That(instance.Value, Is.EqualTo(emailAddress));
        }

        [Test]
        public void GivenIncorrectEmailAddress_ShouldThrowException()
        {
            const string emailAddress = "testtest.com";

            Assert.Catch<ArgumentException>(() => { EmailAddress.Create(emailAddress); });
        }
    }
}