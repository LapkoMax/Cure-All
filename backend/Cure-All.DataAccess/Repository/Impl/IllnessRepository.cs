using Cure_All.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository.Impl
{
    public class IllnessRepository : RepositoryBase<Illness>, IIllnessRepository
    {
        public IllnessRepository(DataContext dataContext) : base(dataContext)
        {}

        public async Task<IEnumerable<Illness>> GetAllIllnesesAsync(bool trackChanges = false)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<Illness> GetIllnessByIdAsync(Guid illnessId, bool trackChanges = false)
        {
            return await FindByCondition(ill => ill.Id.Equals(illnessId)).SingleOrDefaultAsync();
        }

        public void CreateIllness(Illness illness) => Create(illness);

        public void DeleteIllness(Illness illness) => Delete(illness);
    }
}
