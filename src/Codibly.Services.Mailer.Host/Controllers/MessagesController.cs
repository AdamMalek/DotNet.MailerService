using System.Collections.Generic;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Commands;
using Codibly.Services.Mailer.Application.Dto;
using Codibly.Services.Mailer.Application.Queries;
using Codibly.Services.Mailer.Domain.Model;
using Codibly.Services.Mailer.Host.Dto;
using Codibly.Services.Mailer.Host.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Codibly.Services.Mailer.Host.Controllers
{
    [ApiController]
    [HandleDomainException]
    [Route("api/messages")]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public MessagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<EmailMessageDto>> Get()
        {
            return await this.mediator.Send(new GetAllEmailMessages());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateMessageDto request)
        {
            await this.mediator.Send(new CreateEmailMessage(request.Subject, request.Body, request.IsHtml,
                request.Sender, false, request.Recipients));
            return this.Ok();
        }

        [HttpGet("{id}/details")]
        public async Task<EmailMessageDto> GetDetails(string id)
        {
            return await this.mediator.Send(new GetEmailMessageDetails(new EmailMessageId(id)));
        }

        [HttpGet("{id}/status")]
        public async Task<GetStatusResponseDto> GetStatus(string id)
        {
            var status = await this.mediator.Send(new GetEmailMessageStatus(new EmailMessageId(id)));
            return new GetStatusResponseDto(id, status);
        }
    }
}