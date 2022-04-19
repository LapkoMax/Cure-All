using AutoMapper;
using Cure_All.DataAccess.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Notification
{
    public class CreateAppointmentConfirmNotificationCommand : IRequest<Guid>
    {
        public Guid notificationId { get; set; }
    }

    public class CreateAppointmentConfirmNotificationCommandHandler : IRequestHandler<CreateAppointmentConfirmNotificationCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        public CreateAppointmentConfirmNotificationCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateAppointmentConfirmNotificationCommand command, CancellationToken cancellationToken)
        {
            var notification = await _repository.Notification.GetNotificationAsync(command.notificationId, true);
            
            var appointment = await _repository.Appointment.GetAppointmentAsync((Guid)notification.AppointmentId);
            
            var patientCard = await _repository.PatientCard.GetPatientCardByIdAsync(appointment.PatientCardId);

            var newNotification = new Models.Entities.Notification
            {
                UserId = patientCard.Patient.UserId,
                Readed = false,
                Message = $"Ваше посещение подтверждено",
                AppointmentId = notification.AppointmentId,
                ShowFrom = DateTime.UtcNow
            };

            _repository.Notification.CreateNotification(newNotification);

            _repository.Notification.DeleteNotification(notification);

            await _repository.SaveAsync();

            if(newNotification.Id != Guid.Empty)
            {
                var newDoctorNotification = new Models.Entities.Notification
                {
                    UserId = appointment.Doctor.UserId,
                    Readed = false,
                    Message = $"Напоминание о предстоящем посещении",
                    AppointmentId = appointment.Id,
                    ShowFrom = appointment.StartDate.AddDays(-1)
                };

                _repository.Notification.CreateNotification(newDoctorNotification);

                var newPatientNotification = new Models.Entities.Notification
                {
                    UserId = patientCard.Patient.UserId,
                    Readed = false,
                    Message = $"Напоминание о предстоящем посещении",
                    AppointmentId = appointment.Id,
                    ShowFrom = appointment.StartDate.AddDays(-1)
                };

                _repository.Notification.CreateNotification(newPatientNotification);

                await _repository.SaveAsync();
            }

            return newNotification.Id;
        }
    }
}
