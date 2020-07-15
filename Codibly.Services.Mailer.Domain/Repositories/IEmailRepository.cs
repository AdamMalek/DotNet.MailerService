using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Domain.Repositories
{
    public interface IEmailRepository
    {
        Task<EmailMessage> GetMessageByIdAsync(string id);

        Task InsertMessageAsync(EmailMessage message);
        Task UpdateMessageAsync(EmailMessage message);
    }
}