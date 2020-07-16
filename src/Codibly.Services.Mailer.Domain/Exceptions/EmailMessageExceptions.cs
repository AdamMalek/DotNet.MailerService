using System;
using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Domain.Exceptions
{
    public class EmailMessageException : DomainException
    {
        public EmailMessageException(EmailMessageId messageId, string message) : base($"Message {messageId}: {message}")
        {
        }
    }

    public class InvalidEmailMessageException : EmailMessageException
    {
        public InvalidEmailMessageException(EmailMessageId messageId) : base(messageId, "Invalid Email message")
        {
        }
    }

    public class NoSenderException : EmailMessageException
    {
        public NoSenderException(EmailMessageId messageId) : base(messageId, "Sender cannot be empty")
        {
        }
    }

    public class NoRecipientsException : EmailMessageException
    {
        public NoRecipientsException(EmailMessageId messageId) : base(messageId, "Recipients list cannot be empty")
        {
        }
    }

    public class EmptySubjectException : EmailMessageException
    {
        public EmptySubjectException(EmailMessageId messageId) : base(messageId, "Subject cannot be empty")
        {
        }
    }

    public class MessageAlreadySentException : EmailMessageException
    {
        public MessageAlreadySentException(EmailMessageId messageId) : base(messageId, "Message was already sent")
        {
        }
    }
}