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
    public class DoctorsSceduleRepository : RepositoryBase<DoctorsScedule>, IDoctorsSceduleRepository
    {
        public DoctorsSceduleRepository(DataContext dataContext) : base(dataContext)
        { }

        public override IQueryable<DoctorsScedule> FindAll(bool trackChanges = false) =>
            !trackChanges ?
            DataContext.DoctorsScedules.Include(docSced => docSced.Doctor).ThenInclude(doc => doc.User).AsNoTracking() :
            DataContext.DoctorsScedules.Include(docSced => docSced.Doctor).ThenInclude(doc => doc.User);

        public override IQueryable<DoctorsScedule> FindByCondition(Expression<Func<DoctorsScedule, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            DataContext.DoctorsScedules.Include(docSced => docSced.Doctor).ThenInclude(doc => doc.User).Where(expression).AsNoTracking() :
            DataContext.DoctorsScedules.Include(docSced => docSced.Doctor).ThenInclude(doc => doc.User).Where(expression);

        public async Task<IEnumerable<DoctorsScedule>> GetDoctorsScedule(Guid doctorId, bool trackChanges = false)
        {
            return await FindByCondition(docSced => docSced.DoctorId.Equals(doctorId), trackChanges).ToListAsync();
        }

        public void CreateDoctorsScedule(DoctorsScedule doctorsScedule) => Create(doctorsScedule);

        public void DeleteDoctorsScedule(DoctorsScedule doctorsScedule) => Delete(doctorsScedule);
    }
}
