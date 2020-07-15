using System;
using System.Collections.Generic;
using System.Linq;

namespace Codibly.Services.Mailer.Domain.Model
{
    public class EmailMessage
    {
        public string Id { get; private set; }
        public string Subject { get; private set; }
        public MessageBody Body { get; private set; }

        public EmailAddress Sender { get; private set; }
        private readonly HashSet<EmailAddress> recipients = new HashSet<EmailAddress>();

        public IEnumerable<EmailAddress> Recipients => this.recipients.AsEnumerable();
        // Skipped Cc & Bcc for simplicity     
        protected EmailMessage(){}
        public EmailMessage(string subject, MessageBody body, EmailAddress sender, IEnumerable<EmailAddress> recipients)
        {
            this.Subject = subject;
            this.Body = body;
            this.Sender = sender;
            this.recipients = new HashSet<EmailAddress>(recipients ?? Enumerable.Empty<EmailAddress>());
        }
        public static EmailMessage CreatePending(string subject,
            MessageBody body,
            EmailAddress sender,
            IEnumerable<EmailAddress> recipients)
            => CreateMessage(subject, body, sender, recipients, true);

        private static EmailMessage CreateMessage(string subject,
            MessageBody body,
            EmailAddress sender,
            IEnumerable<EmailAddress> recipients,
            bool pending)
        {
            var message = new EmailMessage(subject, body, sender, recipients);
            if (!pending)
            {
                message.FinalizeMessage();
            }

            return message;
        }

        public void FinalizeMessage()
        {
            if (this.recipients.Any() == false) throw new Exception();
            if (this.Sender is null) throw new Exception();
            if (this.Body is null) throw new Exception();
            if (string.IsNullOrWhiteSpace(Subject)) throw new Exception();
        }

        public void MarkAsSent()
        {
        }

        public void AddRecipient(EmailAddress address)
        {
            this.recipients.Add(address);
        }
    }
}