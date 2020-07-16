using System;

namespace Codibly.Services.Mailer.Domain.Exceptions
{
    public class InvalidEmailAddressException : DomainException
    {
        public InvalidEmailAddressException(string emailAddress) : base($"{emailAddress} is not correct email address")
        {
        }
    }
}