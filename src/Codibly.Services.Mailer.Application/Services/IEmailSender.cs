using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Dto;

namespace Codibly.Services.Mailer.Application.Services
{
    public interface IEmailSender
    {
        Task SendAsync(FinalizedEmailMessageDto message);
    }
}