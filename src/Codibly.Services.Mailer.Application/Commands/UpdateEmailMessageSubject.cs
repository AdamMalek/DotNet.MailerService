using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using MediatR;

namespace Codibly.Services.Mailer.Application.Commands
{
    public class UpdateEmailMessageSubject : ICommand
    {
        public UpdateEmailMessageSubject(EmailMessageId id, string subject)
        {
            Id = id;
            Subject = subject;
        }

        public EmailMessageId Id { get; }
        public string Subject { get; }
        
        class Handler: CommandHandler<UpdateEmailMessageSubject, Unit>
        {
            public Handler(IEmailRepository repository): base(repository)
            {
            }
            
            public override async Task<Unit> Handle(UpdateEmailMessageSubject command, CancellationToken cancellationToken)
            {
                var message = await this.GetMessageById(command.Id);
                
                message.UpdateSubject(command.Subject);
                await this.emailRepository.UpdateMessageAsync(message);
                
                return Unit.Value;
            }
        }
    }
}