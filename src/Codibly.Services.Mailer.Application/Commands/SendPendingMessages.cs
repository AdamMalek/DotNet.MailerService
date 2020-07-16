using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using MediatR;

namespace Codibly.Services.Mailer.Application.Commands
{
    public class SendPendingMessages: ICommand
    {
       class Handler: ICommandHandler<SendPendingMessages, Unit>
       {
           private readonly IEmailRepository emailRepository;

           public Handler(IEmailRepository emailRepository)
           {
               this.emailRepository = emailRepository;
           }
           
           public async Task<Unit> Handle(SendPendingMessages request, CancellationToken cancellationToken)
           {
               var pendingMessages = await this.emailRepository.GetPendingMessages();

               var errorMessages = new List<EmailMessageId>();
               var successCount = 0;
               foreach (var message in pendingMessages)
               {
                   
               }
               
               return Unit.Value;
           }
       }
    }
}