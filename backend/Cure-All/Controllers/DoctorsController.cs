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

        public DoctorsController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
