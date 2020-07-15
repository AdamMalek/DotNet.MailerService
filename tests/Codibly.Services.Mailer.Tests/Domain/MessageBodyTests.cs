using System;
using System.Linq;
using Codibly.Services.Mailer.Domain.Exceptions;
using Codibly.Services.Mailer.Domain.Model;
using NUnit.Framework;

namespace Codibly.Services.Mailer.Tests.Domain
{
    public class MessageBodyTests
    {
        [Test]
        public void GivenCorrectBody_WhenCreatingHtmlBodyInstance_ShouldSucceed()
        {
            var bodyText = "<p>This is correct body</p>";

            var body = MessageBody.CreateHtmlBody(bodyText);

            Assert.That(body, Is.Not.Null);
            Assert.That(body.IsHtml, Is.True);
            Assert.That(body.Body, Is.EqualTo(bodyText));
        }

        [Test]
        public void GivenCorrectBody_WhenCreatingTextBodyInstance_ShouldSucceed()
        {
            var bodyText = "This is correct body";

            var body = MessageBody.CreateTextBody(bodyText);

            Assert.That(body, Is.Not.Null);
            Assert.That(body.IsHtml, Is.False);
            Assert.That(body.Body, Is.EqualTo(bodyText));
        }

        [Test]
        public void GivenIncorrectBody_WhenCreatingHtmlBodyInstance_ShouldFail(
            [Values("", null, "                 ", "\n")]
            string body)
        {
            Assert.Catch<EmptyMessageBodyException>(() => { MessageBody.CreateHtmlBody(body); });
        }

        [Test]
        public void GivenIncorrectBody_WhenCreatingTextBodyInstance_ShouldFail(
            [Values("", null, "                 ", "\n")]
            string body)
        {
            Assert.Catch<EmptyMessageBodyException>(() => { MessageBody.CreateTextBody(body); });
        }
    }
}