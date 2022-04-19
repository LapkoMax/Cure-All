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
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(DataContext dataContext) : base(dataContext)
        {}

        public override IQueryable<Patient> FindAll(bool trackChanges = false) =>
            !trackChanges ?
            DataContext.Patients.Include(pat => pat.User).Include(pat => pat.PatientCard).AsNoTracking() :
            DataContext.Patients.Include(pat => pat.User).Include(pat => pat.PatientCard);

        public override IQueryable<Patient> FindByCondition(Expression<Func<Patient, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            DataContext.Patients.Include(pat => pat.User).Include(pat => pat.PatientCard).Where(expression).AsNoTracking() :
            DataContext.Patients.Include(pat => pat.User).Include(pat => pat.PatientCard).Where(expression);

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync(bool trackChanges = false)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<Patient> GetPatientByPatientIdAsync(Guid patientId, bool trackChanges = false)
        {
            return await FindByCondition(pat => pat.Id.Equals(patientId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<Patient> GetPatientByUserIdAsync(string userId, bool trackChanges = false)
        {
            return await FindByCondition(pat => pat.UserId.Equals(userId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<int> GetPatientAmountAsync()
        {
            return await FindAll().CountAsync();
        }

        public void CreatePatient(Patient patient) => Create(patient);

        public void DeletePatient(Patient patient) => Delete(patient);
    }
}
