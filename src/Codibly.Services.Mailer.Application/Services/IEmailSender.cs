using System.Threading.Tasks;

namespace Codibly.Services.Mailer.Application.Services
{
    public interface IEmailSender
    {
        Task SendAsync();
    }
}