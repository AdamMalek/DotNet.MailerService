using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Dto;
using Codibly.Services.Mailer.Application.Services;
using Codibly.Services.Mailer.Infrastructure.Options;

namespace Codibly.Services.Mailer.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderOptions options;


        public EmailSender(EmailSenderOptions options)
        {
            this.options = options;
        }

        public async Task SendAsync(FinalizedEmailMessageDto emailMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(emailMessage.Sender),
                Subject = emailMessage.Subject,
                Body = emailMessage.Body,
                IsBodyHtml = emailMessage.IsHtml,
            };

            foreach (var recipient in emailMessage.Recipients)
            {
                message.To.Add(recipient);
            }

            using var client = this.GetClient();
            await client.SendMailAsync(message);
        }

        private SmtpClient GetClient()
        {
            return new SmtpClient
            {
                Host = this.options.Host,
                Port = this.options.Port,
                Credentials = new NetworkCredential(this.options.Username, this.options.Password),
                EnableSsl = this.options.UseSsl
            };
        }
    }
}