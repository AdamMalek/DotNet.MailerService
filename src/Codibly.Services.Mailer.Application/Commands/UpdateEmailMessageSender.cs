using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using MediatR;

namespace Codibly.Services.Mailer.Application.Commands
{
    public class UpdateEmailMessageSender : ICommand
    {
        public UpdateEmailMessageSender(EmailMessageId id, string sender)
        {
            this.Id = id;
            this.Sender = sender;
        }

        public EmailMessageId Id { get; }
        public string Sender { get; }

        class Handler : CommandHandler<UpdateEmailMessageSender, Unit>
        {
            public Handler(IEmailRepository repository) : base(repository)
            {
            }

            public override async Task<Unit> Handle(UpdateEmailMessageSender command, CancellationToken cancellationToken)
            {
                var message = await this.GetMessageById(command.Id);
                
                message.UpdateSender(EmailAddress.Create(command.Sender));
                await this.emailRepository.UpdateMessageAsync(message);

                return Unit.Value;
            }
        }
    }
}