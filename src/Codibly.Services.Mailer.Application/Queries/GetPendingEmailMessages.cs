using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Commands;
using Codibly.Services.Mailer.Application.Dto;
using Codibly.Services.Mailer.Domain.Repositories;

namespace Codibly.Services.Mailer.Application.Queries
{
    public class GetPendingEmailMessages : ICommand<ICollection<EmailMessageDto>>
    {
        class Handler : ICommandHandler<GetPendingEmailMessages, ICollection<EmailMessageDto>>
        {
            private readonly IEmailRepository emailRepository;

            public Handler(IEmailRepository emailRepository)
            {
                this.emailRepository = emailRepository;
            }

            public async Task<ICollection<EmailMessageDto>> Handle(GetPendingEmailMessages request,
                CancellationToken cancellationToken)
            {
                return (await this.emailRepository.GetPendingMessages())
                    .Select(x => new EmailMessageDto(x)).ToList();
            }
        }
    }
}