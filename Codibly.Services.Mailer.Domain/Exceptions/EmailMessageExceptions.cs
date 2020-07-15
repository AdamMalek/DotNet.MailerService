using System;

namespace Codibly.Services.Mailer.Domain.Exceptions
{
    public class EmailMessageException : DomainException
    {
        public EmailMessageException(string message) : base(message)
        {
        }
    }

    public class InvalidEmailMessageException : EmailMessageException
    {
        public InvalidEmailMessageException() : base("Invalid Email message")
        {
        }
    }

    public class NoSenderException : EmailMessageException
    {
        public NoSenderException() : base("Email message has no sender")
        {
        }
    }

    public class NoRecipientsException : EmailMessageException
    {
        public NoRecipientsException() : base("Email message has no recipients")
        {
        }
    }

    public class EmptySubjectException : EmailMessageException
    {
        public EmptySubjectException() : base("Email message cannot be empty")
        {
        }
    }

    public class MessageAlreadySentException : EmailMessageException
    {
        public MessageAlreadySentException() : base("Email message was already sent")
        {
        }
    }
}