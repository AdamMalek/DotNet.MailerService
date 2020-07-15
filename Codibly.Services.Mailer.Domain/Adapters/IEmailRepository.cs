using System.Collections.Generic;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Domain.Adapters
{
    public interface IEmailRepository
    {
        IEnumerable<EmailMessage> GetAllMessages();
        IEnumerable<EmailMessage> GetPendingMessages();

        Task SaveMessageAsync(EmailMessage message);
    }
}