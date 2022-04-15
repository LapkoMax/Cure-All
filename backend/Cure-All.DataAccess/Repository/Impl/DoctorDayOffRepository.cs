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
    public class DoctorDayOffRepository : RepositoryBase<DoctorDayOffs>, IDoctorDayOffRepository
    {
        public DoctorDayOffRepository(DataContext dataContext): base(dataContext)
        { }

        public override IQueryable<DoctorDayOffs> FindAll(bool trackChanges = false) =>
            !trackChanges ?
            DataContext.DoctorDayOffs.Include(docDayOff => docDayOff.Doctor).ThenInclude(doc => doc.User).AsNoTracking() :
            DataContext.DoctorDayOffs.Include(docDayOff => docDayOff.Doctor).ThenInclude(doc => doc.User);

        public override IQueryable<DoctorDayOffs> FindByCondition(Expression<Func<DoctorDayOffs, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            DataContext.DoctorDayOffs.Include(docDayOff => docDayOff.Doctor).ThenInclude(doc => doc.User).Where(expression).AsNoTracking() :
            DataContext.DoctorDayOffs.Include(docDayOff => docDayOff.Doctor).ThenInclude(doc => doc.User).Where(expression);

        public async Task<IEnumerable<DoctorDayOffs>> GetDoctorsDayOffsByDoctorId(Guid doctorId, bool trackChanges = false)
        {
            return await FindByCondition(docDayOff => docDayOff.DoctorId.Equals(doctorId)).ToListAsync();
        }

        public async Task<IEnumerable<DoctorDayOffs>> GetDoctorsDayOffsByStatus(DoctorStatusEnum status, bool trackChanges = false)
        {
            return await FindByCondition(docDayOff => docDayOff.Status.Equals(status)).ToListAsync();
        }

        public void CreateDoctorDayOffs(DoctorDayOffs doctorDayOff) => Create(doctorDayOff);

        public void DeleteDoctorDayOffs(DoctorDayOffs doctorDayOff) => Delete(doctorDayOff);
    }
}
