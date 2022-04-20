using AutoMapper;
using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Notification
{
    public class CreateAppointmentRejectNotificationCommand : IRequest<Guid>
    {
        public Guid notificationId { get; set; }
    }

    public class CreateAppointmentRejectNotificationCommandHandler : IRequestHandler<CreateAppointmentRejectNotificationCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        public CreateAppointmentRejectNotificationCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateAppointmentRejectNotificationCommand command, CancellationToken cancellationToken)
        {
            var notification = await _repository.Notification.GetNotificationAsync(command.notificationId, true);
            
            var appointment = await _repository.Appointment.GetAppointmentAsync((Guid)notification.AppointmentId, true);

            var patientCard = await _repository.PatientCard.GetPatientCardByIdAsync(appointment.PatientCardId);

            var newNotification = new Models.Entities.Notification
            {
                UserId = patientCard.Patient.UserId,
                Readed = false,
                Message = $"Ваше посещение на {appointment.StartDate.Date.ToShortDateString()} {appointment.StartTime} отклонено. Доктор: {appointment.DoctorId}. ",
                ShowFrom = DateTime.UtcNow
            };

            _repository.Notification.CreateNotification(newNotification);

            _repository.Notification.DeleteNotification(notification);

            await _repository.SaveAsync();

            if (newNotification.Id != Guid.Empty)
                _repository.Appointment.DeleteAppointment(appointment);

            await _repository.SaveAsync();

            return newNotification.Id;
        }
    }
}
