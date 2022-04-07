using AutoMapper;
using Cure_All.BusinessLogic.Extensions;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Doctor;
using Cure_All.MediatRCommands.Patient;
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
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public IdentityController(IIdentityService identityService, IMapper mapper, IMediator mediator)
        {
            _identityService = identityService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("/userByLogin")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserByLogin(string userLogin)
        {
            var user = await _identityService.GetUserAsync(userLogin);

            if (user == null)
                return NotFound();
            else if (user.Id != HttpContext.GetUserId()) 
                return BadRequest(new { errors = new string[]{ "Ошибка авторизации!" } });

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(UserRegistrationDto registrationDto)
        {
            var authResult = await _identityService.RegisterAsync(registrationDto);

            if (!authResult.Success)
                return BadRequest(new { Errors = authResult.ErrorMessages });

            return Ok(new { Token = authResult.Token });
        }

        [HttpPost("/registerDoctor")]
        public async Task<IActionResult> RegisterDoctor(DoctorForCreationDto registrationDto)
        {
            var authResult = await _identityService.RegisterAsync(registrationDto);

            if (!authResult.Success)
                return BadRequest(new { Errors = authResult.ErrorMessages });

            var user = await _identityService.GetUserAsync(registrationDto.UserName);

            var newDoctorId = await _mediator.Send(new CreateDoctorCommand { doctor = registrationDto, userId = user.Id }, CancellationToken.None);

            var newDoctor = await _mediator.Send(new GetDoctorCommand { doctorId = newDoctorId }, CancellationToken.None);

            if(newDoctor.UserName != registrationDto.UserName)
                return BadRequest(new { Errors = new string[]{ "Что-то пошло не так!" } });

            return Ok(new { Token = authResult.Token });
        }

        [HttpPost("/registerPatient")]
        public async Task<IActionResult> RegisterPatient(PatientForCreationDto registrationDto)
        {
            var authResult = await _identityService.RegisterAsync(registrationDto);

            if (!authResult.Success)
                return BadRequest(new { Errors = authResult.ErrorMessages });

            var user = await _identityService.GetUserAsync(registrationDto.UserName);

            var newPatientId = await _mediator.Send(new CreatePatientCommand { patient = registrationDto, userId = user.Id }, CancellationToken.None);

            var newPatient = await _mediator.Send(new GetPatientCommand { patientId = newPatientId }, CancellationToken.None);

            if(newPatient.UserName != registrationDto.UserName)
                return BadRequest(new { Errors = new string[] { "Что-то пошло не так!" } });

            return Ok(new { Token = authResult.Token });
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var authResult = await _identityService.LoginAsync(loginDto);

            if (!authResult.Success)
                return BadRequest(new { Errors = authResult.ErrorMessages });

            return Ok(new { Token = authResult.Token });
        }

        [HttpPost("/activeUserId")]
        [Authorize]
        public IActionResult GetActiveUserId()
        {
            return Ok(new { UserId = HttpContext.GetUserId() });
        }
    }
}
