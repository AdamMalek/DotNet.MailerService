using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Codibly.Services.Mailer.Tests")]
namespace Codibly.Services.Mailer.Domain.Model
{
    public class EmailAddress : IEquatable<EmailAddress>
    {
        public string Value { get; private set; }

        protected EmailAddress(){}
        internal EmailAddress(string value)
        {
            this.Value = value;
        }

        internal static EmailAddress Create(string emailAddress)
        {
            if (IsValid(emailAddress) == false)
            {
                throw new ArgumentException("Provided e-mail address is not correct", nameof(emailAddress));
            }
            
            return new EmailAddress(emailAddress);
        }

        #region Validation
        private static bool IsValid(string value)
        {
            // https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format:w
            try
            {
                return Regex.IsMatch(value,
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
    }
}
