using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IRepositoryManager
    {
        IDoctorRepository Doctor { get; }

        IDoctorsSceduleRepository DoctorsScedule { get; }

        IDoctorDayOffRepository DoctorDayOff { get; }

        IPatientRepository Patient { get; }

        IPatientCardRepository PatientCard { get; }

        ISpecializationRepository Specialization { get; }

        IIllnessRepository Illness { get; }

        IPatientIllnesesRepository PatientIllneses { get; }

        IAppointmentRepository Appointment { get; }

        INotificationRepository Notification { get; }

        Task SaveAsync();
    }
}
