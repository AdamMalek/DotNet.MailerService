using System;
using Codibly.Services.Mailer.Domain.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Codibly.Services.Mailer.Infrastructure.Repositories.Mongo.Documents
{
    public class MessageDocument
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string Subject { get; set; }
        public MessageBodyDocument Body { get; set; }
        public string Sender { get; set; }
        public MessageStatus Status { get; set; }
        public string[] Recipients { get; set; }
    }

    public class MessageBodyDocument
    {
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}