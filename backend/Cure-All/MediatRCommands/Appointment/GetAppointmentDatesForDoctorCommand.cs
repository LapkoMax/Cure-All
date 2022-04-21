using Cure_All.DataAccess.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Appointment
{
    public class GetAppointmentDatesForDoctorCommand : IRequest<IEnumerable<DateTime>>
    {
        public string userId { get; set; }
    }

    public class GetAppointmentDatesForDoctorCommandHandler : IRequestHandler<GetAppointmentDatesForDoctorCommand, IEnumerable<DateTime>>
    {
        private readonly IRepositoryManager _repository;

        public GetAppointmentDatesForDoctorCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DateTime>> Handle(GetAppointmentDatesForDoctorCommand command, CancellationToken cancellationToken)
        {
            var doctor = await _repository.Doctor.GetDoctorByUserIdAsync(command.userId);

            if (doctor == null)
                return null;

            return await _repository.Appointment.GetAppointmentDatesForDoctorAsync(doctor.Id);
        }
    }
}
