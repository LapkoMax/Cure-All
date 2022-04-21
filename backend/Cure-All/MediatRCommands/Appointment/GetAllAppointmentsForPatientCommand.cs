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
    public class GetAllAppointmentsForPatientCommand : IRequest<IEnumerable<AppointmentDto>>
    {
        public Guid patientCardId { get; set; }
    }

    public class GetAllAppointmentsForPatientCommandHandler : IRequestHandler<GetAllAppointmentsForPatientCommand, IEnumerable<AppointmentDto>>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetAllAppointmentsForPatientCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> Handle(GetAllAppointmentsForPatientCommand command, CancellationToken cancellationToken)
        {
            var appointments = await _repository.Appointment.GetAllAppointmentsForPatientAsync(command.patientCardId);

            var appointmentsToReturn = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return appointmentsToReturn;
        }
    }
}
