using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Exceptions;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using MediatR;

namespace Codibly.Services.Mailer.Application.Commands
{
    public class UpdateEmailMessageBody : ICommand
    {
        public UpdateEmailMessageBody(EmailMessageId id, string body, bool isHtmlBody)
        {
            Id = id;
            Body = body;
            IsHtmlBody = isHtmlBody;
        }

        public EmailMessageId Id { get; }
        public string Body { get; }
        public bool IsHtmlBody { get; }
        
        class Handler: ICommandHandler<UpdateEmailMessageBody, Unit>
        {
            private readonly IEmailRepository repository;

            public Handler(IEmailRepository repository)
            {
                this.repository = repository;
            }
            public async Task<Unit> Handle(UpdateEmailMessageBody command, CancellationToken cancellationToken)
            {
                var message = await this.repository.GetMessageByIdAsync(command.Id);
                if (message == null)
                {
                    throw new InvalidEmailMessageException(command.Id);
                }

                var body = command.IsHtmlBody
                    ? MessageBody.CreateHtmlBody(command.Body)
                    : MessageBody.CreateTextBody(command.Body);
                
                message.UpdateBody(body);
                await this.repository.UpdateMessageAsync(message);
                
                return Unit.Value;
            }
        }
    }
}