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
        public async Task<ActionResult> Post([FromBody] CreateMessageDto request)
        {
            await this.mediator.Send(new CreateEmailMessage(request.Subject, request.Body, request.IsHtml,
                request.Sender, true, request.Recipients));
            return this.Ok();
        }

        [HttpPost("send")]
        public async Task<SendPendingMessagesResultDto> SendPending()
        {
            var result = await this.mediator.Send(new SendPendingMessages());
            return new SendPendingMessagesResultDto(result.SuccessCount, result.Errors);
        }

        [HttpPatch("{id}/subject")]
        public async Task<ActionResult> ChangeSubject(string id, [FromBody] UpdateSubjectRequest request)
        {
            await this.mediator.Send(new UpdateEmailMessageSubject(new EmailMessageId(id), request.Subject));
            return this.Ok();
        }

        [HttpPatch("{id}/body")]
        public async Task<ActionResult> ChangeBody(string id, [FromBody] UpdateBodyRequest request)
        {
            await this.mediator.Send(
                new UpdateEmailMessageBody(new EmailMessageId(id), request.Body, request.IsHtml));
            return this.Ok();
        }

        [HttpPatch("{id}/sender")]
        public async Task<ActionResult> ChangeSender(string id, [FromBody] UpdateSenderRequest request)
        {
            await this.mediator.Send(
                new UpdateEmailMessageSender(new EmailMessageId(id), request.Sender));
            return this.Ok();
        }

        [HttpPost("{id}/recipients")]
        public async Task<ActionResult> AddRecipients(string id, [FromBody] AddRecipientsRequest request)
        {
            if (request.Recipients?.Length <= 0)
            {
                return this.BadRequest();
            }

            await this.mediator.Send(
                new AddEmailMessageRecipients(new EmailMessageId(id), request.Recipients));
            return this.Ok();
        }
    }
}