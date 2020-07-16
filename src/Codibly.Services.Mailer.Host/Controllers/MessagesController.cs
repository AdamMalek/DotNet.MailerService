using System.Collections.Generic;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Commands;
using Codibly.Services.Mailer.Host.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Codibly.Services.Mailer.Host.Controllers
{
    [ApiController]
    [HandleDomainException]
    [Route("api/[controller]")]
    public class MessagesController
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<int>> Get()
        {
            return new[] {1};
        }


        [HttpGet("/pending")]
        public async Task<IEnumerable<int>> GetPending()
        {
            return new[] {1, 2};
        }

        [HttpPost("/pending")]
        public async Task<IEnumerable<int>> PostPending()
        {
            return new[] {1, 2};
        }
        //
        // [HttpGet("/test")]
        // public async Task<int> Post()
        // {
        //     await this._mediator.Send(new CreateEmailMessage(null, null, null, null, false, null));
        //     return 1;
        // }
    }
}