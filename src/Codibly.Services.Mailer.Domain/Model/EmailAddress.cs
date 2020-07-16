using System;
using System.Text.RegularExpressions;
using Codibly.Services.Mailer.Domain.Exceptions;

namespace Codibly.Services.Mailer.Domain.Model
{
    public class EmailAddress : IEquatable<EmailAddress>
    {
        public string Value { get; private set; }

        protected EmailAddress()
        {
        }

        internal EmailAddress(string value)
        {
            this.Value = value;
        }

        public static EmailAddress Create(string emailAddress)
        {
            if (emailAddress is null) return null;

            if (IsValid(emailAddress) == false)
            {
                throw new InvalidEmailAddressException(emailAddress);
            }

            return new EmailAddress(emailAddress);
        }

        #region Validation

        private static bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            // https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format:w
            try
            {
                return Regex.IsMatch(value, ".+@.+\\..+", RegexOptions.IgnoreCase,
                           TimeSpan.FromMilliseconds(50)) // high-level, fast validation
                       && Regex.IsMatch(value,
                           @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                           RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        #endregion

        public override int GetHashCode() => this.Value.GetHashCode();

        public bool Equals(EmailAddress other) =>
            this.Value.Equals(other?.Value, StringComparison.InvariantCultureIgnoreCase);

        public override string ToString() => this.Value;
    }
}