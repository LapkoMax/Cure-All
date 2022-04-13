using Cure_All.MediatRCommands.Illness;
using Cure_All.Models.DTO;
using Cure_All.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.Controllers
{
    [Route("api/illneses")]
    [ApiController]
    [Authorize]
    public class IllnesesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IllnesesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Illness>> GetIllnesses()
        {
            return await _mediator.Send(new GetIllnesesCommand { }, CancellationToken.None);
        }

        [HttpPost]
        public async Task<IActionResult> CreteIllness(IllnessForCreationDto illness)
        {
            var newIllnessId = await _mediator.Send(new CreateNewIllnessCommand { illness = illness }, CancellationToken.None);

            var illnessEntity = await _mediator.Send(new GetIllnessCommand { illnessId = newIllnessId }, CancellationToken.None);

            if (illnessEntity == null)
                return BadRequest();
            return Ok();
        }
    }
}
