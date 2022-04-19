using Cure_All.DataAccess.Repository;
using Cure_All.Models.DTO;
using Cure_All.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cure_All.MediatRCommands.Doctor
{
    public class GetAvailableAppointmentTimeForDateCommand : IRequest<IEnumerable<AvailableAppointmentTimeDto>>
    {
        public DateTime date { get; set; }

        public Guid doctorId { get; set; }
    }

    public class GetAvailableAppointmentTimeForDateCommandHandler : IRequestHandler<GetAvailableAppointmentTimeForDateCommand, IEnumerable<AvailableAppointmentTimeDto>>
    {
        private readonly IRepositoryManager _repository;

        public GetAvailableAppointmentTimeForDateCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AvailableAppointmentTimeDto>> Handle(GetAvailableAppointmentTimeForDateCommand command, CancellationToken cancellationToken)
        {
            var availableTime = new List<AvailableAppointmentTimeDto>();

            var doctor = await _repository.Doctor.GetDoctorByDoctorIdAsync(command.doctorId);

            if (doctor == null)
                return availableTime;

            var doctorAppointments = await _repository.Appointment.GetAllAppointmentsForDoctorAsync(command.doctorId);

            var appointmentsTime = doctorAppointments.Where(app => app.StartDate.Date.Equals(command.date.Date)).Select(app => app.StartTime);

            var doctorScedule = await _repository.DoctorsScedule.GetDoctorsScedule(command.doctorId);

            var doctorWorkingDays = string.Join(" ", doctorScedule.Select(sced => sced.dayOfWeek.ToString()));

            if (!doctorWorkingDays.Contains(command.date.DayOfWeek.ToString()) || command.date.Date < DateTime.UtcNow.Date)
                return availableTime;

            var doctorDayOffs = await _repository.DoctorDayOff.GetDoctorsDayOffsByDoctorId(command.doctorId);

            var unavailableDays = doctorDayOffs.Where(day => day.Status != DoctorStatusEnum.Available).Select(day => day.Date);

            foreach (var date in unavailableDays)
                if (command.date.Date.Equals(date.Date)) return availableTime;

            var time = doctor.WorkDayStart;

            while(time < doctor.WorkDayEnd)
            {
                if (DateTime.UtcNow.Date == command.date.Date && DateTime.Now.TimeOfDay > time) 
                {
                    time = time.Add(new TimeSpan(0, doctor.AverageAppointmentTime, 0)); 
                    continue;
                }

                bool available = true;

                foreach (var appTime in appointmentsTime)
                    if (time >= appTime && time <= appTime.Add(new TimeSpan(0, doctor.AverageAppointmentTime, 0))) available = false;

                if (available && time > doctor.DinnerStart.Subtract(new TimeSpan(0, doctor.AverageAppointmentTime, 0)) && time < doctor.DinnerEnd) available = false;

                if (available) availableTime.Add(new AvailableAppointmentTimeDto
                {
                    doctorId = command.doctorId,
                    Time = $"{time.Hours}:{time.Minutes}"
                });

                time = time.Add(new TimeSpan(0, doctor.AverageAppointmentTime, 0));
            }

            return availableTime;
        }
    }
}
