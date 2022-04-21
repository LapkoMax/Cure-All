using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Appointment
{
    public class GetAppointmentDatesForPatientCommand : IRequest<IEnumerable<DateTime>>
    {
        public Guid patientCardId { get; set; }
    }

    public class GetAppointmentDatesForPatientCommandHandler : IRequestHandler<GetAppointmentDatesForPatientCommand, IEnumerable<DateTime>>
    {
        private readonly IRepositoryManager _repository;

        public GetAppointmentDatesForPatientCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DateTime>> Handle(GetAppointmentDatesForPatientCommand command, CancellationToken cancellationToken)
        {
            return await _repository.Appointment.GetAppointmentDatesForPatientAsync(command.patientCardId);
        }
    }
}
