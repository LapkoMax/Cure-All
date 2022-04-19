using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Patient;
using Cure_All.Models.DTO;
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
    [Route("api/patients")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IRepositoryManager _repository;

        private readonly IIdentityService _identityService;

        public PatientsController(IMediator mediator, IIdentityService identityService, IRepositoryManager repository)
        {
            _mediator = mediator;
            _identityService = identityService;
            _repository = repository;
        }

        [HttpGet("{patientId}")]
        public async Task<ActionResult<PatientDto>> GetPatient(Guid patientId)
        {
            var patient = await _mediator.Send(new GetPatientCommand { patientId = patientId }, CancellationToken.None);

            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<DoctorDto>> GetPatientFromUser(string userId)
        {
            var patient = await _mediator.Send(new GetPatientCommand { userId = userId }, CancellationToken.None);

            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        [HttpGet("amount")]
        public async Task<IActionResult> GetPatientAmount()
        {
            return Ok(await _repository.Patient.GetPatientAmountAsync());
        }

        [HttpPut("{patientId}")]
        public async Task<IActionResult> EditPatient(Guid patientId, PatientForEditingDto patient)
        {
            var user = await _identityService.GetUserAsync(patient.OldUserName);

            var patientEntity = await _mediator.Send(new GetPatientCommand { patientId = patientId }, CancellationToken.None);

            if (user == null || patientEntity == null) return NotFound();
            else if (patientEntity.UserId != user.Id) return Unauthorized();

            var errors = await _identityService.EditUserAsync(patient);

            if (errors.Count() > 0) return BadRequest(new { Errors = errors });

            await _mediator.Send(new EditPatientCommand { patient = patient, patientId = patientId }, CancellationToken.None);

            return Ok();
        }
    }
}
