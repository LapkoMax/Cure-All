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
    public class GetTodayAppointmentsForPatientCommand : IRequest<IEnumerable<AppointmentDto>>
    {
        public Guid patientCardId { get; set; }
    }

    public class GetTodayAppointmentsForPatientCommandHandler : IRequestHandler<GetTodayAppointmentsForPatientCommand, IEnumerable<AppointmentDto>>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetTodayAppointmentsForPatientCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> Handle(GetTodayAppointmentsForPatientCommand command, CancellationToken cancellationToken)
        {
            var appointments = await _repository.Appointment.GetTodaysAppointmentsForPatientAsync(command.patientCardId);

            var appointmentsToReturn = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return appointmentsToReturn;
        }
    }
}
