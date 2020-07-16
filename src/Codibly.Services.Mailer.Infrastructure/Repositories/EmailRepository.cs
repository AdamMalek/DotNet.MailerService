using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using Codibly.Services.Mailer.Infrastructure.Options;

namespace Codibly.Services.Mailer.Infrastructure.Repositories
{
    public class EmailRepository: IEmailRepository, IEmailQueueRepository
    {
        public EmailRepository(MongoConnectionString connectionString)
        {
            
        }
        
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

        public async Task<IEnumerable<EmailMessage>> GetQueuedMessages()
        {
            return new[]
            {
                new EmailMessage("Test", MessageBody.CreateTextBody("fdsfds"), EmailAddress.Create("test@test.com"),
                    new[] {EmailAddress.Create("mupdtqwzbaqubuwrcx@ttirv.org")}, MessageStatus.Queued,
                    new EmailMessageId("test"))
            };
        }
        
        public async Task<IEnumerable<EmailMessage>> GetPendingMessages()
        {
            return Enumerable.Empty<EmailMessage>();
        }

        public async Task<IEnumerable<EmailMessage>> GetAllMessages()
        {
            return Enumerable.Empty<EmailMessage>();
        }
    }
}