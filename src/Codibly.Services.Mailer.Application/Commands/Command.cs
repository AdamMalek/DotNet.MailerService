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
}