using System.Collections.Generic;
using System.Linq;

namespace Codibly.Services.Mailer.Domain.Model
{
    public class EmailMessage
    {
        public string Id { get; private set; }
        public string Subject { get; private set; }
        public EmailAddress Sender { get; private set; }
        private readonly HashSet<EmailAddress> recipients;
        public IEnumerable<EmailAddress> Recipients => this.recipients.AsEnumerable();

        // Skipped Cc & Bcc for simplicity 
        
        public void AddRecipient(EmailAddress address)
        {
            this.recipients.Add(address);
        }
    }
}