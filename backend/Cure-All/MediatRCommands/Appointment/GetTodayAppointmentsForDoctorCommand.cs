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
    public class GetTodayAppointmentsForDoctorCommand : IRequest<IEnumerable<AppointmentDto>>
    {
        public string userId { get; set; }
    }

    public class GetTodayAppointmentsForDoctorCommandHandler : IRequestHandler<GetTodayAppointmentsForDoctorCommand, IEnumerable<AppointmentDto>> 
    {
        private readonly IRepositoryManager _repository;

        private readonly IMapper _mapper;

        public GetTodayAppointmentsForDoctorCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> Handle(GetTodayAppointmentsForDoctorCommand command, CancellationToken cancellationToken)
        {
            var doctor = await _repository.Doctor.GetDoctorByUserIdAsync(command.userId);

            var appointments = await _repository.Appointment.GetTodaysAppointmentsForDoctorAsync(doctor.Id);

            var appointmentsToReturn = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return appointmentsToReturn;
        }
    }

}
