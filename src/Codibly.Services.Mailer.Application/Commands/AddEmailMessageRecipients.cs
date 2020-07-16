using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using MediatR;

namespace Codibly.Services.Mailer.Application.Commands
{
    public class AddEmailMessageRecipients : ICommand
    {
        public AddEmailMessageRecipients(EmailMessageId messageId, IEnumerable<string> recipients)
        {
            MessageId = messageId;
            Recipients = recipients;
        }

        public EmailMessageId MessageId { get; }
        public IEnumerable<string> Recipients { get; }

        class Handler : CommandHandler<AddEmailMessageRecipients, Unit>
        {
            public Handler(IEmailRepository emailRepository) : base(emailRepository)
            {
            }

            public override async Task<Unit> Handle(AddEmailMessageRecipients request,
                CancellationToken cancellationToken)
            {
                var message = await this.GetMessageById(request.MessageId);
                var recipients = request.Recipients
                    .Where(x => x != null)
                    .Select(EmailAddress.Create);
                foreach (var recipient in recipients)
                {
                    message.AddRecipient(recipient);
                }

                await this.emailRepository.UpdateMessageAsync(message);
                
                return Unit.Value;
            }
        }
    }
}