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
    public class PatientCardRepository : RepositoryBase<PatientCard>, IPatientCardRepository
    {
        public PatientCardRepository(DataContext dataContext) : base(dataContext)
        {}

        public override IQueryable<PatientCard> FindAll(bool trackChanges = false) =>
            !trackChanges ?
            DataContext.PatientCards.Include(pc => pc.Patient).ThenInclude(pat => pat.User).Include(pc => pc.Appointments).AsNoTracking() :
            DataContext.PatientCards.Include(pc => pc.Patient).ThenInclude(pat => pat.User).Include(pc => pc.Appointments);

        public override IQueryable<PatientCard> FindByCondition(Expression<Func<PatientCard, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            DataContext.PatientCards.Include(pc => pc.Patient).ThenInclude(pat => pat.User).Include(pc => pc.Appointments).Where(expression).AsNoTracking() :
            DataContext.PatientCards.Include(pc => pc.Patient).ThenInclude(pat => pat.User).Include(pc => pc.Appointments).Where(expression);

        public async Task<IEnumerable<PatientCard>> GetAllPatientCardsAsync(bool trackChanges = false)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<PatientCard> GetPatientCardByIdAsync(Guid patientCardId, bool trackChanges = false)
        {
            return await FindByCondition(pc => pc.Id.Equals(patientCardId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<PatientCard> GetPatientCardForPatientAsync(Guid patientId, bool trackChanges = false)
        {
            return await FindByCondition(pc => pc.PatientId.Equals(patientId), trackChanges).SingleOrDefaultAsync();
        }

        public void CreatePatientCardForPatient(Guid patientId, PatientCard patientCard)
        {
            patientCard.PatientId = patientId;
            Create(patientCard);
        }

        public void DeletePatientCard(PatientCard patientCard) => Delete(patientCard);
    }
}
