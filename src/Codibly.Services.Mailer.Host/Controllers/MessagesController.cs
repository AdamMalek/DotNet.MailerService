using System.Collections.Generic;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Codibly.Services.Mailer.Host.Controllers
{
    [ApiController]
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

        [HttpPost]
        public async Task<int> Post()
        {
            await this._mediator.Send(new CreateEmailMessage(null, null, null, null, true, null));
            return 1;
        }
    }
}