using System.Threading.Tasks;
using MediatR;

namespace Codibly.Services.Mailer.Domain.Commands
{
    public interface ICommand: IRequest
    {
    }
    
    public interface ICommandHandler<in T> : IRequestHandler<T> where T : IRequest
    {
    }
}