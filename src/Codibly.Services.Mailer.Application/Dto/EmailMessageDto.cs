using System.Linq;
using Codibly.Services.Mailer.Domain.Exceptions;
using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Application.Dto
{
    public class EmailMessageDto
    {
        public EmailMessageDto(EmailMessage message)
        {
            this.Id = message.Id.ToString();
            this.Subject = message.Subject;
            this.Body = message.Body?.Body;
            this.IsHtml = message.Body?.IsHtml;
            this.Sender = message.Sender?.Value;
            this.Status = message.Status.ToString();
            this.Recipients = message.Recipients.Select(x => x.ToString()).ToArray();
        }

        public string Id { get; }
        public string Subject { get; }
        public string Body { get; }
        public bool? IsHtml { get; }
        public string Sender { get; }
        public string Status { get; set; }
        public string[] Recipients { get; }
    }
}