using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Exceptions;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using MediatR;

namespace Codibly.Services.Mailer.Application.Commands
{
    public interface ICommand: IRequest<Unit>
    {
    }
    
    public interface ICommand<out TResult>: IRequest<TResult>
    {
    }
    
    public interface ICommandHandler<in T,TResult> : IRequestHandler<T,TResult> where T : IRequest<TResult>
    {
    }
    
    public abstract class CommandHandler<TCommand, TResult>: ICommandHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {
        protected readonly IEmailRepository emailRepository;

        public CommandHandler(IEmailRepository emailRepository)
        {
            this.emailRepository = emailRepository;
        }

        protected async Task<EmailMessage> GetMessageById(EmailMessageId messageId)
        {
            var message = await this.emailRepository.GetMessageByIdAsync(messageId);
            if (message == null)
            {
                throw new InvalidEmailMessageException(messageId);
            }

            return message;
        }

        public abstract Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);
    }
}