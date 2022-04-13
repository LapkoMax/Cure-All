using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsForPatientAsync(Guid patientCardId, bool trackChanges = false);

        Task<IEnumerable<Appointment>> GetAllAppointmentsForDoctorAsync(Guid doctorId, bool trackChanges = false);

        Task<Appointment> GetAppointmentAsync(Guid appointmentId, bool trackChanges = false);

        void CreateAppointment(Appointment appointment);

        void DeleteAppointment(Appointment appointment);
    }
}
