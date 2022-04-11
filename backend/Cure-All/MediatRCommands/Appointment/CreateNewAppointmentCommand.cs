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
    public class CreateNewAppointmentCommand : IRequest<Guid>
    {
        public AppointmentForCreationDto appointment;
    }

    public class CreateNewAppointmentCommandHandler : IRequestHandler<CreateNewAppointmentCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public CreateNewAppointmentCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateNewAppointmentCommand command, CancellationToken cancellationToken)
        {
            var appointmentEntity = _mapper.Map<Models.Entities.Appointment>(command.appointment);

            _repository.Appointment.CreateAppointment(appointmentEntity);

            await _repository.SaveAsync();

            return appointmentEntity.Id;
        }
    }
}
