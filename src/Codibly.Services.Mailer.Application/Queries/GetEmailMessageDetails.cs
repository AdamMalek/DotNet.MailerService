using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Commands;
using Codibly.Services.Mailer.Application.Dto;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Domain.Repositories;
using MediatR;

namespace Codibly.Services.Mailer.Application.Queries
{
    public class GetEmailMessageDetails: ICommand<EmailMessageDto>
    {
        public GetEmailMessageDetails(EmailMessageId messageId)
        {
            MessageId = messageId;
        }

        public EmailMessageId MessageId { get; } 
        
        class Handler : CommandHandler<GetEmailMessageDetails, EmailMessageDto>
        {
            public Handler(IEmailRepository emailRepository) : base(emailRepository)
            {
            }

            public override async Task<EmailMessageDto> Handle(GetEmailMessageDetails request, CancellationToken cancellationToken)
            {
                var message = await this.GetMessageById(request.MessageId);
                
                return new EmailMessageDto(message);
            }
        }
    }
}