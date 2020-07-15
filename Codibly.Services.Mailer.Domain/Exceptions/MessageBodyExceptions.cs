using System;

namespace Codibly.Services.Mailer.Domain.Exceptions
{
    public class MessageBodyException : DomainException
    {
        public MessageBodyException(string message) : base(message)
        {
        }
    }

    public class EmptyMessageBodyException : MessageBodyException
    {
        public EmptyMessageBodyException() : base("Message body cannot be empty!")
        {
        }
    }
}