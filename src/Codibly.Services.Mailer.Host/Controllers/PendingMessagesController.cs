using System.Collections.Generic;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Commands;
using Codibly.Services.Mailer.Application.Dto;
using Codibly.Services.Mailer.Application.Queries;
using Codibly.Services.Mailer.Host.Dto;
using Codibly.Services.Mailer.Host.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Codibly.Services.Mailer.Host.Controllers
{
    [ApiController]
    [HandleDomainException]
    [Route("api/messages/pending")]
    public class PendingMessagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PendingMessagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<EmailMessageDto>> Get()
        {
            return await this.mediator.Send(new GetPendingEmailMessages());
        }

        [HttpPost]
        public async Task<ActionResult> PostPending([FromBody] CreateMessageDto request)
        {
            await this.mediator.Send(new CreateEmailMessage(request.Subject, request.Body, request.IsHtml,
                request.Sender, true, request.Recipients));
            return this.Ok();
        }
    }
}