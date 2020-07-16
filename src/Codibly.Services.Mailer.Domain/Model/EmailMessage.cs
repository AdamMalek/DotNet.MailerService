using System.Collections.Generic;
using System.Linq;
using Codibly.Services.Mailer.Domain.Exceptions;

namespace Codibly.Services.Mailer.Domain.Model
{
    public class EmailMessage
    {
        public EmailMessageId Id { get; }
        public string Subject { get; private set; }
        public MessageBody Body { get; private set; }
        public MessageStatus Status { get; private set; }
        public EmailAddress Sender { get; }
        
        private readonly HashSet<EmailAddress> recipients = new HashSet<EmailAddress>();
        public IEnumerable<EmailAddress> Recipients => this.recipients.AsEnumerable();

        // Skipped Cc & Bcc for simplicity     
        protected EmailMessage()
        {
        }

        internal EmailMessage(string subject, MessageBody body, EmailAddress sender, IEnumerable<EmailAddress> recipients,
            MessageStatus status = MessageStatus.Pending, EmailMessageId id = null)
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
            if (this.recipients.Any() == false) throw new NoRecipientsException(this.Id );
            if (this.Sender is null) throw new NoSenderException(this.Id);
            if (this.Body is null) throw new EmptyMessageBodyException(this.Id);
            if (string.IsNullOrWhiteSpace(Subject)) throw new EmptySubjectException(this.Id);

            this.Status = MessageStatus.Queued;
        }

        public void MarkAsSent()
        {
            this.CheckIfNotSent();
            this.Status = MessageStatus.Sent;
        }

        public void AddRecipient(EmailAddress address)
        {
            this.CheckIfNotSent();
            this.recipients.Add(address);
        }

        public void UpdateBody(MessageBody body)
        {
            this.CheckIfNotSent();
            if (body is null)
            {
                throw new EmptyMessageBodyException();
            }

            this.Body = body;
        }

        public void UpdateSubject(string subject)
        {
            this.CheckIfNotSent();
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new EmptySubjectException(this.Id);
            }

            this.Subject = subject;
        }

        private void CheckIfNotSent()
        {
            if (this.Status != MessageStatus.Pending)
            {
                throw new MessageAlreadySentException(this.Id);
            }
        }
    }
}