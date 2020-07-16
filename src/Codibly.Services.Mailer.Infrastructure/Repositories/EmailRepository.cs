using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;

namespace Codibly.Services.Mailer.Infrastructure.Repositories
{
    public class EmailRepository: IEmailRepository
    {
        public Task<EmailMessage> GetMessageByIdAsync(EmailMessageId id)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertMessageAsync(EmailMessage message)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateMessageAsync(EmailMessage message)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<EmailMessage>> GetPendingMessages()
        {
            return Enumerable.Empty<EmailMessage>();
        }
    }
}