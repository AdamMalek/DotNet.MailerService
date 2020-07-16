using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Commands;
using Codibly.Services.Mailer.Application.Dto;
using Codibly.Services.Mailer.Domain.Repositories;

namespace Codibly.Services.Mailer.Application.Queries
{
    public class GetAllEmailMessages : ICommand<ICollection<EmailMessageDto>>
    {
        class Handler : ICommandHandler<GetAllEmailMessages, ICollection<EmailMessageDto>>
        {
            private readonly IEmailRepository emailRepository;

            public Handler(IEmailRepository emailRepository)
            {
                this.emailRepository = emailRepository;
            }

            public async Task<ICollection<EmailMessageDto>> Handle(GetAllEmailMessages request,
                CancellationToken cancellationToken)
            {
                return (await this.emailRepository.GetAllMessages())
                    .Select(x => new EmailMessageDto(x)).ToList();
            }
        }
    }
}