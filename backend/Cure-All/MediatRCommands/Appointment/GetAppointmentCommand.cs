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
    public class GetAppointmentCommand : IRequest<AppointmentDto>
    {
        public Guid appointmentId { get; set; }
    }

    public class GetAppointmentCommandHandler : IRequestHandler<GetAppointmentCommand, AppointmentDto>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetAppointmentCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> Handle(GetAppointmentCommand command, CancellationToken cancellationToken)
        {
            var appointment = await _repository.Appointment.GetAppointmentAsync(command.appointmentId);

            if (appointment == null)
                return null;

            var appointmentToReturn = _mapper.Map<AppointmentDto>(appointment);

            return appointmentToReturn;
        }
    }
}
