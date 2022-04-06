using Cure_All.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository.Impl
{
    public class SpecializationRepository : RepositoryBase<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(DataContext dataContext) : base(dataContext)
        {}

        public async Task<IEnumerable<Specialization>> GetAllSpecializationsAsync(bool trackChanges = false)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<Specialization> GetSpecializationIdAsync(Guid specializationId, bool trackChanges = false)
        {
            return await FindByCondition(spec => spec.Id.Equals(specializationId), trackChanges).SingleOrDefaultAsync();
        }

        public void CreateSpecialization(Specialization specialization) => Create(specialization);

        public void DeleteSpecialization(Specialization specialization) => Delete(specialization);
    }
}
