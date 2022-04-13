using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cure_All.MediatRCommands.PatientCard;
using Cure_All.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cure_All.Controllers
{
    [Route("api/patientCards")]
    [ApiController]
    [Authorize]
    public class PatientCardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientCardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{patientCardId}")]
        public async Task<PatientCardDto> GetPatientCard(Guid patientCardId)
        {
            return await _mediator.Send(new GetPatientCardCommand { patientCardId = patientCardId }, CancellationToken.None);
        }

        [HttpGet("forPatient/{patientId}")]
        public async Task<PatientCardDto> GetCardForPatient(Guid patientId)
        {
            return await _mediator.Send(new GetCardForPatientCommand { patientId = patientId }, CancellationToken.None);
        }
    }
}
