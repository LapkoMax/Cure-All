using Cure_All.BusinessLogic.RequestFeatures;
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

        Task<IEnumerable<Appointment>> GetAppointmentsForPatientAsync(Guid patientCardId, AppointmentParameters parameters, bool trackChanges = false);

        Task<IEnumerable<DateTime>> GetAppointmentDatesForPatientAsync(Guid patientCardId);

        Task<IEnumerable<Appointment>> GetAllAppointmentsForDoctorAsync(Guid doctorId, bool trackChanges = false);

        Task<IEnumerable<Appointment>> GetAppointmentsForDoctorAsync(Guid doctorId, AppointmentParameters parameters, bool trackChanges = false);

        Task<IEnumerable<DateTime>> GetAppointmentDatesForDoctorAsync(Guid doctorId);

        Task<Appointment> GetAppointmentAsync(Guid appointmentId, bool trackChanges = false);

        Task<int> GetAppointmentAmountAsync();

        Task<int> GetCompletedAppointmentAmountAsync();

        void CreateAppointment(Appointment appointment);

        void DeleteAppointment(Appointment appointment);
    }
}
