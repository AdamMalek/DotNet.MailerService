using System.Threading.Tasks;

namespace Codibly.Services.Mailer.Domain.Commands
{
    public interface ICommand
    {
    }

    public interface ICommandHandler
    {
    }

    public interface ICommandHandler<in T> : ICommandHandler where T : ICommand
    {
        Task HandleCommandAsync(T command);
    }
}