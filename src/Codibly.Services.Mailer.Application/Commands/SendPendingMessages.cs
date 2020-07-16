using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Exceptions;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using MediatR;

namespace Codibly.Services.Mailer.Application.Commands
{
    public class SendPendingMessages: ICommand<SendPendingMessagesResult>
    {
       class Handler: ICommandHandler<SendPendingMessages, SendPendingMessagesResult>
       {
           private readonly IEmailRepository emailRepository;

           public Handler(IEmailRepository emailRepository)
           {
               this.emailRepository = emailRepository;
           }
           
           public async Task<SendPendingMessagesResult> Handle(SendPendingMessages request, CancellationToken cancellationToken)
           {
               var pendingMessages = await this.emailRepository.GetPendingMessages();

               var errorMessages = new List<string>();
               var successCount = 0;
               foreach (var message in pendingMessages)
               {
                   try
                   {
                       message.FinalizeMessage();
                       successCount++;
                   }
                   catch (DomainException e)
                   {
                       errorMessages.Add(e.Message);
                   } 
               }
               
               return new SendPendingMessagesResult(successCount, errorMessages);
           }
       }
    }

    public class SendPendingMessagesResult
    {
        public SendPendingMessagesResult(int successCount, ICollection<string> errors)
        {
            SuccessCount = successCount;
            Errors = errors;
        }

        public int SuccessCount { get; }
        public ICollection<string> Errors { get; }
    }
}