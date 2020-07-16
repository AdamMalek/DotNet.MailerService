using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Exceptions;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using Codibly.Services.Mailer.Infrastructure.Options;
using Codibly.Services.Mailer.Infrastructure.Repositories.Mongo.Documents;
using MongoDB.Driver;

namespace Codibly.Services.Mailer.Infrastructure.Repositories.Mongo
{
    public class MongoEmailRepository : IEmailRepository, IEmailQueueRepository
    {
        public MongoEmailRepository(MongoConnectionString connectionString)
        {
            var client = new MongoClient(connectionString.MongoDbConnectionString);
            this.Collection = client.GetDatabase("Mailer").GetCollection<MessageDocument>("messages");
        }

        public IMongoCollection<MessageDocument> Collection { get; }

        public async Task<EmailMessage> GetMessageByIdAsync(EmailMessageId id)
        {
            var message = this.Collection.Find(x => x.Id.Equals(id.ToString()));
            return (await message.SingleOrDefaultAsync())?.AsEntity();
        }

        public async Task InsertMessageAsync(EmailMessage message)
        {
            await this.Collection.InsertOneAsync(message.AsDocument());
        }

        public async Task UpdateMessageAsync(EmailMessage message)
        {
            await this.Collection.FindOneAndReplaceAsync(document => document.Id.Equals(message.Id.ToString()),
                message.AsDocument());
        }

        public async Task<IEnumerable<EmailMessage>> GetQueuedMessages()
        {
            return this.Collection
                .AsQueryable()
                .Where(x => x.Status == MessageStatus.Queued)
                .AsEnumerable()
                .Select(x => x.AsEntity());
        }

        public async Task<IEnumerable<EmailMessage>> GetPendingMessages()
        {
            return this.Collection
                .AsQueryable()
                .Where(x => x.Status == MessageStatus.Pending)
                .AsEnumerable()
                .Select(x => x.AsEntity());
        }

        public async Task<IEnumerable<EmailMessage>> GetAllMessages()
        {
            return this.Collection
                .AsQueryable()
                .AsEnumerable()
                .Select(x => x.AsEntity());
        }
    }
}