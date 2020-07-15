using System.Threading.Tasks;

namespace Codibly.Services.Mailer.Domain.Commands
{
    public interface ICommand
    {
    }

    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleCommandAsync(T command);
    }
}