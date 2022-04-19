using Cure_All.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository.Impl
{
    public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(DataContext dataContext) : base(dataContext)
        {}

        public override IQueryable<Appointment> FindAll(bool trackChanges = false) =>
            !trackChanges ?
            DataContext.Appointments.Include(app => app.PatientCard).ThenInclude(card => card.Patient).ThenInclude(pat => pat.User).Include(app => app.Doctor).ThenInclude(doc => doc.User).Include(app => app.Illness).AsNoTracking() :
            DataContext.Appointments.Include(app => app.PatientCard).ThenInclude(card => card.Patient).ThenInclude(pat => pat.User).Include(app => app.Doctor).ThenInclude(doc => doc.User).Include(app => app.Illness);

        public override IQueryable<Appointment> FindByCondition(Expression<Func<Appointment, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            DataContext.Appointments.Include(app => app.PatientCard).ThenInclude(card => card.Patient).ThenInclude(pat => pat.User).Include(app => app.Doctor).ThenInclude(doc => doc.User).Include(app => app.Illness).Where(expression).AsNoTracking() :
            DataContext.Appointments.Include(app => app.PatientCard).ThenInclude(card => card.Patient).ThenInclude(pat => pat.User).Include(app => app.Doctor).ThenInclude(doc => doc.User).Include(app => app.Illness).Where(expression);

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsForDoctorAsync(Guid doctorId, bool trackChanges = false)
        {
            return await FindByCondition(app => app.DoctorId.Equals(doctorId), trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetTodaysAppointmentsForDoctorAsync(Guid doctorId, bool trackChanges = false)
        {
            return await FindByCondition(app => app.DoctorId.Equals(doctorId) && app.StartDate.Date == DateTime.UtcNow.Date, trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsForPatientAsync(Guid patientCardId, bool trackChanges = false)
        {
            return await FindByCondition(app => app.PatientCardId.Equals(patientCardId), trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetTodaysAppointmentsForPatientAsync(Guid patientCardId, bool trackChanges = false)
        {
            return await FindByCondition(app => app.PatientCardId.Equals(patientCardId) && app.StartDate.Date == DateTime.UtcNow.Date, trackChanges).ToListAsync();
        }

        public async Task<Appointment> GetAppointmentAsync(Guid appointmentId, bool trackChanges = false)
        {
            return await FindByCondition(app => app.Id.Equals(appointmentId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<int> GetAppointmentAmountAsync()
        {
            return await FindAll().CountAsync();
        }

        public async Task<int> GetCompletedAppointmentAmountAsync()
        {
            return await FindByCondition(app => app.Completed).CountAsync();
        }

        public void CreateAppointment(Appointment appointment) => Create(appointment);

        public void DeleteAppointment(Appointment appointment) => Delete(appointment);
    }
}
