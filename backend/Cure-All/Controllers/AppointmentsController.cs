using Cure_All.DataAccess.Repository;
using Cure_All.MediatRCommands.Appointment;
using Cure_All.MediatRCommands.Notification;
using Cure_All.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IRepositoryManager _repository;

        public AppointmentsController(IMediator mediator, IRepositoryManager repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet("forDoctor/{userId}")]
        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsForDoctor(string userId)
        {
            return await _mediator.Send(new GetAppointmentsForDoctorCommand { userId = userId }, CancellationToken.None);
        }

        [HttpGet("forPatient/{patientCardId}")]
        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsForPatient(Guid patientCardId)
        {
            return await _mediator.Send(new GetAppointmentsForPatientCommand { patientCardId = patientCardId }, CancellationToken.None);
        }

        [HttpGet("{appointmentId}")]
        public async Task<AppointmentDto> GetAppointment(Guid appointmentId)
        {
            return await _mediator.Send(new GetAppointmentCommand { appointmentId = appointmentId }, CancellationToken.None);
        }

        [HttpGet("{appointmentId}/canUserChange/{userId}")]
        public async Task<bool> CanUserChangeAppointment(Guid appointmentId, string userId)
        {
            var appointment = await _repository.Appointment.GetAppointmentAsync(appointmentId);

            return appointment.Doctor.UserId == userId;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentForCreationDto appointment)
        {
            var notificationForAppointment = await _mediator.Send(new CreateNewAppointmentCommand { appointment = appointment }, CancellationToken.None);

            if (notificationForAppointment == null || notificationForAppointment.AppointmentId == null || notificationForAppointment.AppointmentId == Guid.Empty)
                return BadRequest(new { Errors = new string[] { "Что-то пошло не так" } });

            var newNotificationId = await _mediator.Send(new CreateNotificationCommand { notification = notificationForAppointment }, CancellationToken.None);

            if (newNotificationId == Guid.Empty)
                return BadRequest(new { Errors = new string[] { "Что-то пошло не так" } });

            return Ok(notificationForAppointment.AppointmentId);
        }

        [HttpPut("{appointmentId}")]
        public async Task<IActionResult> EditAppointment(Guid appointmentId, AppointmentForEditingDto appointment)
        {
            await _mediator.Send(new EditAppointmentCommand { appointment = appointment, appointmentId = appointmentId }, CancellationToken.None);

            return Ok();
        }

        [HttpDelete("{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment(Guid appointmentId)
        {
            var result = await _mediator.Send(new DeleteAppointmentCommand { appointmentId = appointmentId }, CancellationToken.None);

            if (!result) return BadRequest();

            return NoContent();
        }
    }
}
