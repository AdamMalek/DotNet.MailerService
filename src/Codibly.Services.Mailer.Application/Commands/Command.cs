using MediatR;

namespace Codibly.Services.Mailer.Application.Commands
{
    public interface ICommand: IRequest
    {
    }
    
    public interface ICommandHandler<in T> : IRequestHandler<T> where T : IRequest
    {
    }
}