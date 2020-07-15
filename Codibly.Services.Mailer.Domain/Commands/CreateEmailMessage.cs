using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public class Handler : ICommandHandler<CreateEmailMessage>
        {
            private readonly IEmailRepository repository;

            public Handler()
            {
                this.repository = null; //repository;
            }

            public async Task HandleCommandAsync(CreateEmailMessage command)
            {
                var messageBody = GetMessageBody(command.Body, command.IsHtmlBody);
                var sender = EmailAddress.Create(command.Sender);
                var recipients = command.Recipients?
                    .Select(EmailAddress.Create)
                    .Where(x => !(x is null));

                var message = command.IsPendingEmail
                    ? EmailMessage.CreatePending(command.Subject, messageBody, sender, recipients)
                    : EmailMessage.Create(command.Subject, messageBody, sender, recipients);

                await this.repository.InsertMessageAsync(message);
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
}