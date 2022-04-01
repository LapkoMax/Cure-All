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
    public class PatientIllnesesRepository : RepositoryBase<PatientIllneses>, IPatientIllnesesRepository
    {
        public PatientIllnesesRepository(DataContext dataContext) : base(dataContext)
        {}

        public override IQueryable<PatientIllneses> FindByCondition(Expression<Func<PatientIllneses, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            DataContext.PatientIllneses.Include(pi => pi.Illness).Where(expression).AsNoTracking() :
            DataContext.PatientIllneses.Include(pi => pi.Illness).Where(expression);

        public async Task<IEnumerable<Illness>> GetAllPatientIllnesesAsync(Guid patientCardId, bool trackChanges = false)
        {
            var patientIllneses = await FindByCondition(pi => pi.PatientCardId.Equals(patientCardId), trackChanges).ToListAsync();
            var illneses = new List<Illness>();
            foreach (var patientIllness in patientIllneses)
                illneses.Add(patientIllness.Illness);
            return illneses;
        }

        public void AddIllnessToPatientCard(PatientIllneses patientIllneses) => Create(patientIllneses);

        public void DeleteIllnessFromPatientCard(PatientIllneses patientIllneses) => Delete(patientIllneses);
    }
}
