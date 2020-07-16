using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using MediatR;

namespace Codibly.Services.Mailer.Application.Commands
{
    public class MarkMessagesAsSent: ICommand
    {
        public IEnumerable<EmailMessageId> MessageIds { get; }

        public MarkMessagesAsSent(IEnumerable<EmailMessageId> messageIds)
        {
            this.MessageIds = messageIds;
        }
        
        public class Handler: ICommandHandler<MarkMessagesAsSent>
        {
            private readonly IEmailRepository emailRepository;

            public Handler(IEmailRepository emailRepository)
            {
                this.emailRepository = emailRepository;
            }
            
            public async Task<Unit> Handle(MarkMessagesAsSent request, CancellationToken cancellationToken)
            {
                foreach (var messageId in request.MessageIds)
                {
                    var message = await this.emailRepository.GetMessageByIdAsync(messageId);
                    message.MarkAsSent();
                    await this.emailRepository.UpdateMessageAsync(message);
                }
                return Unit.Value;
            }
        }
    }
}