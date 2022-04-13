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
    public class GetAppointmentsForPatientCommand : IRequest<IEnumerable<AppointmentDto>>
    {
        public Guid patientCardId { get; set; }
    }

    public class GetAppointmentsForPatientCommandHandler : IRequestHandler<GetAppointmentsForPatientCommand, IEnumerable<AppointmentDto>>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetAppointmentsForPatientCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> Handle(GetAppointmentsForPatientCommand command, CancellationToken cancellationToken)
        {
            var appointments = await _repository.Appointment.GetAllAppointmentsForPatientAsync(command.patientCardId);

            var appointmentsToReturn = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return appointmentsToReturn;
        }
    }
}
