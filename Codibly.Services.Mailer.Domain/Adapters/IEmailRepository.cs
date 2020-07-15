using System.Collections.Generic;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Domain.Adapters
{
    public interface IEmailRepository
    {
        Task<EmailMessage> GetMessageByIdAsync(string id);

        Task InsertMessageAsync(EmailMessage message);
        Task UpdateMessageAsync(EmailMessage message);
    }
}