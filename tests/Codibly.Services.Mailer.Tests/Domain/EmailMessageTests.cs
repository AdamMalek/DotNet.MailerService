using System.Linq;
using Codibly.Services.Mailer.Domain.Exceptions;
using Codibly.Services.Mailer.Domain.Model;
using NUnit.Framework;

namespace Codibly.Services.Mailer.Tests.Domain
{
    public class EmailMessageTests
    {
        [Test]
        public void GivenCompleteEmail_WhenFinalizing_ShouldSucceed()
        {
            var recipient = new EmailAddress("test@test.com");

            var message = EmailMessage.CreatePending(subject: "Test",
                body: MessageBody.CreateTextBody("This is test message"),
                sender: new EmailAddress("sender@test.com"),
                recipients: new[] {recipient});

            Assert.DoesNotThrow(() => { message.FinalizeMessage(); });
        }

        [Test]
        public void GivenEmailWithoutSubject_WhenFinalizing_ShouldFail()
        {
            var recipient = new EmailAddress("test@test.com");

            var message = EmailMessage.CreatePending(subject: null,
                body: MessageBody.CreateTextBody("This is test message"),
                sender: new EmailAddress("sender@test.com"),
                recipients: new[] {recipient});

            Assert.Catch<EmptySubjectException>(() => { message.FinalizeMessage(); });
        }

        [Test]
        public void GivenEmailWithoutBody_WhenFinalizing_ShouldFail()
        {
            var recipient = new EmailAddress("test@test.com");

            var message = EmailMessage.CreatePending(subject: "Test",
                body: null,
                sender: new EmailAddress("sender@test.com"),
                recipients: new[] {recipient});

            Assert.Catch<EmptyMessageBodyException>(() => { message.FinalizeMessage(); });
        }

        [Test]
        public void GivenEmailWithoutSender_WhenFinalizing_ShouldFail()
        {
            var recipient = new EmailAddress("test@test.com");

            var message = EmailMessage.CreatePending(subject: "Test",
                body: MessageBody.CreateTextBody("This is test message"),
                sender: null,
                recipients: new[] {recipient});

            Assert.Catch<NoSenderException>(() => { message.FinalizeMessage(); });
        }

        [Test]
        public void GivenEmailWithoutRecipients_WhenFinalizing_ShouldSucceed()
        {
            var recipient = new EmailAddress("test@test.com");

            var message = EmailMessage.CreatePending(subject: "Test",
                body: MessageBody.CreateTextBody("This is test message"),
                sender: new EmailAddress("sender@test.com"),
                recipients: null);

            Assert.Catch<NoRecipientsException>(() => { message.FinalizeMessage(); });
        }
        
        [Test]
        public void GivenTwoSameRecipients_ShouldStoreOne()
        {
            const string emailAddress = "test@test.com";
            var recipient = new EmailAddress(emailAddress);
            var recipient2 = new EmailAddress(emailAddress);

            var message = EmailMessage.CreatePending(subject: "Test",
                body: MessageBody.CreateTextBody("This is test message"),
                sender: new EmailAddress("sender@test.com"),
                recipients: new[] {recipient});

            message.AddRecipient(recipient2);

            Assert.That(message.Recipients.Count(), Is.EqualTo(1));
        }
    }
}