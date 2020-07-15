using System.Collections.Generic;
using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Domain.Adapters
{
    public interface IEmailRepository
    {
        IEnumerable<EmailMessage> GetAllMessages();
        IEnumerable<EmailMessage> GetPendingMessages();
        
    }
}