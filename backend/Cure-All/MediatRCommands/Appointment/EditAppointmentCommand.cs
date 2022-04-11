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
    public class EditAppointmentCommand : IRequest<Guid>
    {
        public AppointmentForEditingDto appointment { get; set; }

        public Guid appointmentId { get; set; }
    }

    public class EditAppointmentCommandHandler : IRequestHandler<EditAppointmentCommand, Guid>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public EditAppointmentCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(EditAppointmentCommand command, CancellationToken cancellationToken)
        {
            var appointmentEntity = await _repository.Appointment.GetAppointmentAsync(command.appointmentId, true);

            _mapper.Map(command.appointment, appointmentEntity);

            await _repository.SaveAsync();

            return appointmentEntity.Id;
        }
    }
}
