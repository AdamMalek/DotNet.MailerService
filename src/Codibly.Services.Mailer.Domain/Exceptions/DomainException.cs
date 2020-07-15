using System;

namespace Codibly.Services.Mailer.Domain.Exceptions
{
    public class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}