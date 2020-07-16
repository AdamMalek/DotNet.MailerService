using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Exceptions;
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
        
        class Handler: ICommandHandler<UpdateEmailMessageSubject>
        {
            private readonly IEmailRepository repository;

            public Handler(IEmailRepository repository)
            {
                this.repository = repository;
            }
            public async Task<Unit> Handle(UpdateEmailMessageSubject command, CancellationToken cancellationToken)
            {
                var message = await this.repository.GetMessageByIdAsync(command.Id);
                if (message == null)
                {
                    throw new InvalidEmailMessageException();
                }
                
                message.UpdateSubject(command.Subject);
                await this.repository.UpdateMessageAsync(message);
                
                return Unit.Value;
            }
        }
    }
}