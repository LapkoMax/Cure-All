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
    public class GetAppointmentsForDoctorCommand : IRequest<IEnumerable<AppointmentDto>>
    {
        public string userId { get; set; }
    }

    public class GetAppointmentsForDoctorCommandHandler : IRequestHandler<GetAppointmentsForDoctorCommand, IEnumerable<AppointmentDto>>
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetAppointmentsForDoctorCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> Handle(GetAppointmentsForDoctorCommand command, CancellationToken cancellationToken)
        {
            var doctor = await _repository.Doctor.GetDoctorByUserIdAsync(command.userId);

            var appointments = await _repository.Appointment.GetAllAppointmentsForDoctorAsync(doctor.Id);

            var appointmentsToReturn = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return appointmentsToReturn;
        }
    }
}
