using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.XPath;
using Codibly.Services.Mailer.Domain.Adapters;
using Codibly.Services.Mailer.Domain.Model;

namespace Codibly.Services.Mailer.Domain.Commands
{
    public class CreateEmailMessage : ICommand
    {
        public string Subject { get; }
        public string Body { get; }
        public bool? IsHtmlBody { get; }
        public string Sender { get; }
        public bool IsPendingEmail { get; }
        public IEnumerable<string> Recipients { get; }

        public CreateEmailMessage(string subject, string body, bool? isHtmlBody, string sender,
            bool isPendingEmail, IEnumerable<string> recipients)
        {
            this.Subject = subject;
            this.Body = body;
            this.IsHtmlBody = isHtmlBody;
            this.Sender = sender;
            this.IsPendingEmail = isPendingEmail;
            this.Recipients = recipients;
        }
    }

    class Handler : ICommandHandler<CreateEmailMessage>
    {
        private readonly IEmailRepository repository;

        public Handler(IEmailRepository repository)
        {
            this.repository = repository;
        }

        public async Task HandleCommandAsync(CreateEmailMessage command)
        {
            var messageBody = GetMessageBody(command.Body, command.IsHtmlBody);
            var sender = command.Sender != null ? EmailAddress.Create(command.Sender) : null;
            var recipients = command.Recipients?.Select(EmailAddress.Create);

            var message = command.IsPendingEmail
                ? EmailMessage.CreatePending(command.Subject, messageBody, sender, recipients)
                : EmailMessage.Create(command.Subject, messageBody, sender, recipients);

            await this.repository.SaveMessageAsync(message);
        }

        private static MessageBody GetMessageBody(string body, bool? isHtml) =>
            (isHtml.HasValue, isHtml.HasValue && isHtml.Value) switch
            {
                (true, true) => MessageBody.CreateHtmlBody(body),
                (true, false) => MessageBody.CreateTextBody(body),
                (false, _) => null
            };
    }
}