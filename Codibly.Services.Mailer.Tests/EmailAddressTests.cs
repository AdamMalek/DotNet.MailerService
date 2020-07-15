using System;
using Codibly.Services.Mailer.Domain.Model;
using NUnit.Framework;

namespace Codibly.Services.Mailer.Tests
{
    public class EmailAddressTests
    {
        [Test]
        public void GivenCorrectEmailAddress_ShouldCreateInstance(
            [Values("test@test.com", "fdsafdasfs132321@gmail.com")] string emailAddress)
        {
            var instance = EmailAddress.Create(emailAddress);

            Assert.That(instance, Is.Not.Null);
            Assert.That(instance.Value, Is.EqualTo(emailAddress));
        }

        [Test]
        public void GivenIncorrectEmailAddress_ShouldThrowException(
            [Values("fdsafds", "", null, "fdsa_pl", "testtest.com", "😀@test.com")] string emailAddress)
        {
            Assert.Catch<ArgumentException>(() => { EmailAddress.Create(emailAddress); });
        }
    }
}