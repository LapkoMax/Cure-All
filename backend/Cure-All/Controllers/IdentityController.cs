using AutoMapper;
using Cure_All.BusinessLogic.Extensions;
using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Doctor;
using Cure_All.MediatRCommands.DoctorDayOff;
using Cure_All.MediatRCommands.DoctorsScedule;
using Cure_All.MediatRCommands.Patient;
using Cure_All.MediatRCommands.PatientCard;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityController(IIdentityService identityService, IMapper mapper, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _identityService = identityService;
            _mapper = mapper;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
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

        [HttpGet("/userById/{userId}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUserById(string userId)
        {
            var user = await _identityService.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(UserRegistrationDto registrationDto)
        {
            var authResult = await _identityService.RegisterAsync(registrationDto);

            if (!authResult.Success)
                return BadRequest(new { Errors = authResult.ErrorMessages });

            return Ok(new { Token = "" });
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

            foreach (var docSced in registrationDto.DoctorsScedule)
                docSced.DoctorId = newDoctorId;

            foreach (var docDayOff in registrationDto.DoctorDayOffs)
                docDayOff.DoctorId = newDoctorId;

            return Ok(new { Token = "" });
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

            var newPatientCardId = await _mediator.Send(new CreatePatientCardForPatientCommand { patientId = newPatientId }, CancellationToken.None);

            var newPatientCard = await _mediator.Send(new GetPatientCardCommand { patientCardId = newPatientCardId }, CancellationToken.None);

            if(newPatientCard == null)
                return BadRequest(new { Errors = new string[] { "Что-то пошло не так!" } });

            return Ok(new { Token = "" });
        }

        [HttpGet("/confirmUserEmail")]
        public async Task<IActionResult> ConfirmUserEmail([FromQuery] string token, [FromQuery] string email)
        {
            var user = await _identityService.GetUserAsync(email);

            if (user == null)
                return BadRequest("Ошибка!");

            var result = await _identityService.ConfirmUserEmail(user, token);

            if(!result)
                return BadRequest("Ошибка!");

            return Ok();
        }

        [HttpGet("/resetPasswordRequest")]
        public async Task<IActionResult> ResetPasswordRequest([FromQuery] string email)
        {
            var result = await _identityService.SendResetPasswordEmail(email);

            if (!result) return BadRequest("Пользователя с такой почтой не найден!");

            return Ok();
        }

        [HttpGet("/resetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromQuery] string email, [FromQuery] string newPassword)
        {
            var user = await _identityService.GetUserAsync(email);

            if (user == null)
                return BadRequest("Ошибка!");

            var result = await _identityService.ResetPassword(user, token, newPassword);

            if (!result)
                return BadRequest("Ошибка!");

            return Ok();
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var authResult = await _identityService.LoginAsync(loginDto);

            if (!authResult.Success)
                return BadRequest(new { Errors = authResult.ErrorMessages });

            return Ok(new { Token = authResult.Token });
        }

        [HttpDelete("/{userLogin}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string userLogin)
        {
            var user = await _identityService.GetUserAsync(userLogin);
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;

            if (currentUserId != user.Id)
                return BadRequest(new { Errors = new string[] { "You are not allowed to delete this user!" } });
            else
            {
                bool result = false;
                if (user.Type == "Doctor") result = await _mediator.Send(new DeleteDoctorCommand { userId = user.Id }, CancellationToken.None);
                else if (user.Type == "Patient") 
                {
                    var patient = await _mediator.Send(new GetPatientCommand { userId = user.Id }, CancellationToken.None);

                    result = await _mediator.Send(new DeletePatientCardCommand { patientId = patient.Id }, CancellationToken.None);

                    if (!result) return BadRequest(new { Errors = new string[] { "Something goes wrong!" } });

                    result = await _mediator.Send(new DeletePatientCommand { userId = user.Id }, CancellationToken.None);
                }

                if (!result) return BadRequest(new { Errors = new string[] { "Something goes wrong!" } });

                result = await _identityService.DeleteUserAsync(user);

                if (!result) return BadRequest(new { Errors = new string[] { "Something goes wrong!" } });
            }
            return NoContent();
        }
    }
}
