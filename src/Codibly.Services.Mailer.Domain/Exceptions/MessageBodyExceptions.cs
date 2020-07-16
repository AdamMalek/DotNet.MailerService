using System;
using Codibly.Services.Mailer.Domain.Model;

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
        public EmptyMessageBodyException(EmailMessageId messageId) : base($"Message {messageId?.ToString() ?? "error"}: Body cannot be empty!")
        {
        }
        
        public EmptyMessageBodyException() : base("Message body cannot be empty!")
        {
        }
    }
}