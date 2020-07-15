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
        public MessageStatus Status { get; private set; }
        public EmailAddress Sender { get; private set; }
        private readonly HashSet<EmailAddress> recipients = new HashSet<EmailAddress>();

        public IEnumerable<EmailAddress> Recipients => this.recipients.AsEnumerable();

        // Skipped Cc & Bcc for simplicity     
        protected EmailMessage()
        {
        }

        internal EmailMessage(string subject, MessageBody body, EmailAddress sender, IEnumerable<EmailAddress> recipients,
            MessageStatus status = MessageStatus.Pending, string id = null)
        {
            this.Id = id;
            this.Subject = subject;
            this.Body = body;
            this.Sender = sender;
            this.recipients = new HashSet<EmailAddress>(recipients ?? Enumerable.Empty<EmailAddress>());
            this.Status = status;
        }

        public static EmailMessage Create(string subject,
            MessageBody body,
            EmailAddress sender,
            IEnumerable<EmailAddress> recipients)
        {
            var message = CreateMessage(subject, body, sender, recipients);
            message.FinalizeMessage();
            return message;
        }

        public static EmailMessage CreatePending(string subject,
            MessageBody body,
            EmailAddress sender,
            IEnumerable<EmailAddress> recipients)
            => CreateMessage(subject, body, sender, recipients);

        private static EmailMessage CreateMessage(string subject,
            MessageBody body,
            EmailAddress sender,
            IEnumerable<EmailAddress> recipients)
            => new EmailMessage(subject, body, sender, recipients);

        public void FinalizeMessage()
        {
            if (this.recipients.Any() == false) throw new ArgumentException(nameof(this.Recipients));
            if (this.Sender is null) throw new ArgumentException(nameof(this.Sender));
            if (this.Body is null) throw new ArgumentException(nameof(this.Body));
            if (string.IsNullOrWhiteSpace(Subject)) throw new ArgumentException(nameof(this.Subject));

            this.Status = MessageStatus.Queued;
        }

        public void MarkAsSent()
        {
            this.Status = MessageStatus.Sent;
        }

        public void AddRecipient(EmailAddress address)
        {
            this.recipients.Add(address);
        }
    }
}