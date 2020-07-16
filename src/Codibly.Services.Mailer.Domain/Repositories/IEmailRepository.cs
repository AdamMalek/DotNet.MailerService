using System.Collections.Generic;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Domain.Repositories
{
    public interface IEmailRepository
    {
        Task<EmailMessage> GetMessageByIdAsync(EmailMessageId id);

        Task InsertMessageAsync(EmailMessage message);
        Task UpdateMessageAsync(EmailMessage message);
        Task<IEnumerable<EmailMessage>> GetPendingMessages();
    }
}