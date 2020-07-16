using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Commands;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;

namespace Codibly.Services.Mailer.Application.Queries
{
    public class GetEmailMessageStatus : ICommand<MessageStatus>
    {
        public GetEmailMessageStatus(EmailMessageId messageId)
        {
            MessageId = messageId;
        }

        public EmailMessageId MessageId { get; }

        class Handler : CommandHandler<GetEmailMessageStatus, MessageStatus>
        {
            public Handler(IEmailRepository emailRepository) : base(emailRepository)
            {
            }

            public override async Task<MessageStatus> Handle(GetEmailMessageStatus request, CancellationToken cancellationToken)
            {
                var message = await this.GetMessageById(request.MessageId);
                return message.Status;
            }
        }
    }
}