using System.Collections.Generic;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Domain.Repositories
{
    public interface IEmailQueueRepository
    {
        Task<IEnumerable<EmailMessage>> GetQueuedMessages();
    }
}