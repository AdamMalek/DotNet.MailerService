using System.Linq;
using Codibly.Services.Mailer.Domain.Exceptions;
using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Application.Dto
{
    public class FinalizedEmailMessageDto
    {
        public FinalizedEmailMessageDto(EmailMessage message)
        {
            this.Body = message.Body.Body;
            this.IsHtml = message.Body.IsHtml;
            this.Subject = message.Subject;
            this.Sender = message.Sender.ToString();
            this.Recipients = message.Recipients.Select(x => x.ToString()).ToArray();
        }
        
        public string Subject { get; }
        public string Body { get; }
        public bool IsHtml { get; }
        public string Sender { get; }
        public string[] Recipients { get; }
    }
}