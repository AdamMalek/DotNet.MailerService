using System.Linq;
using Codibly.Services.Mailer.Domain.Exceptions;
using Codibly.Services.Mailer.Domain.Model;
using MongoDB.Bson;

namespace Codibly.Services.Mailer.Infrastructure.Repositories.Mongo.Documents
{
    public static class Extensions
    {
        public static EmailMessage AsEntity(this MessageDocument document)
        {
            return new EmailMessage(subject: document.Subject,
                body: document.Body != null ? new MessageBody(document.Body.Body, document.Body.IsHtml) : null,
                sender: new EmailAddress(document.Sender),
                recipients: document.Recipients?.Select(x => new EmailAddress(x)),
                status: document.Status,
                id: new EmailMessageId(document.Id.ToString())
            );
        }

        public static MessageDocument AsDocument(this EmailMessage message)
        {
            return new MessageDocument
            {
                Id = message.Id?.ToString(),
                Body = message.Body != null
                    ? new MessageBodyDocument {Body = message.Body.Body, IsHtml = message.Body.IsHtml}
                    : null,
                Recipients = message.Recipients?.Select(x=>  x.Value).ToArray(),
                Sender = message.Sender?.Value,
                Status = message.Status,
                Subject = message.Subject
            };
        }
    }
}