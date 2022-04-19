using AutoMapper;
using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Appointment
{
    public class CreateNewAppointmentCommand : IRequest<NotificationForCreationDto>
    {
        public AppointmentForCreationDto appointment;
    }

    public class CreateNewAppointmentCommandHandler : IRequestHandler<CreateNewAppointmentCommand, NotificationForCreationDto>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateNewAppointmentCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NotificationForCreationDto> Handle(CreateNewAppointmentCommand command, CancellationToken cancellationToken)
        {
            var appointmentEntity = _mapper.Map<Models.Entities.Appointment>(command.appointment);

            _repository.Appointment.CreateAppointment(appointmentEntity);

            await _repository.SaveAsync();

            if (appointmentEntity.Id == Guid.Empty)
                return null;

            var appointment = await _repository.Appointment.GetAppointmentAsync(appointmentEntity.Id);

            var notification = new NotificationForCreationDto
            {
                UserId = appointment.Doctor.UserId,
                Message = $"Вам поступил новый запрос на посещение",
                AppointmentId = appointment.Id,
                ShowFrom = DateTime.UtcNow
            };

            return notification;
        }
    }
}
