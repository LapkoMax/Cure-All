using AutoMapper;
using Cure_All.BusinessLogic.RequestFeatures;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Doctor;
using Cure_All.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DoctorsController(IMediator mediator, IIdentityService identityService, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _identityService = identityService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IEnumerable<DoctorDto>> GetDoctors([FromQuery] DoctorParameters doctorParameters)
        {
            return await _mediator.Send(new GetDoctorsCommand { doctorParameters = doctorParameters }, CancellationToken.None);
        }

        [HttpGet("{doctorId}")]
        public async Task<ActionResult<DoctorDto>> GetDoctor(Guid doctorId)
        {
            var doctor = await _mediator.Send(new GetDoctorCommand { doctorId = doctorId }, CancellationToken.None);
            
            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }

        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorFromUser(string userId)
        {
            var doctor = await _mediator.Send(new GetDoctorFromUserCommand { userId = userId }, CancellationToken.None);

            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }

        [HttpPut("{doctorId}")]
        public async Task<IActionResult> EditDoctor(Guid doctorId, DoctorForEditingDto doctor)
        {
            var user = await _identityService.GetUserAsync(doctor.OldUserName);

            var doctorEntity = await _mediator.Send(new GetDoctorCommand { doctorId = doctorId }, CancellationToken.None);

            if (user == null || doctorEntity == null) return NotFound();
            else if (doctorEntity.UserId != user.Id) return Unauthorized();

            var errors = await _identityService.EditUserAsync(doctor);

            if (errors.Count() > 0) return BadRequest(new { Errors = errors });

            await _mediator.Send(new EditDoctorCommand { doctor = doctor, doctorId = doctorId }, CancellationToken.None);

            return Ok();
        }
    }
}
