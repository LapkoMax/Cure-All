using Cure_All.BusinessLogic.RequestFeatures;
using Cure_All.BusinessLogic.RequestFeatures.Extensions;
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
    public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(DataContext dataContext) : base(dataContext)
        {}

        public override IQueryable<Doctor> FindAll(bool trackChanges = false) =>
            !trackChanges ?
            DataContext.Doctors.Include(doc => doc.User).AsNoTracking() :
            DataContext.Doctors.Include(doc => doc.User);

        public override IQueryable<Doctor> FindByCondition(Expression<Func<Doctor, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
            DataContext.Doctors.Include(doc => doc.User).Where(expression).AsNoTracking() :
            DataContext.Doctors.Include(doc => doc.User).Where(expression);

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync(DoctorParameters doctorParameters, bool trackChanges = false)
        {
            return (await FindAll(trackChanges)
                .Search(doctorParameters.FullNameSearchTerm, doctorParameters.SpecialitySearchTerm)
                .Sort(doctorParameters.OrderBy)
                .ToListAsync())
                .FilterDoctors(doctorParameters.MinExperienceYears);
        }

        public async Task<Doctor> GetDoctorByDoctorIdAsync(Guid doctorId, bool trackChanges = false)
        {
            return await FindByCondition(doc => doc.Id.Equals(doctorId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<Doctor> GetDoctorByUserIdAsync(string userId, bool trackChanges = false)
        {
            return await FindByCondition(doc => doc.UserId.Equals(userId), trackChanges).SingleOrDefaultAsync();
        }

        public void CreateDoctor(Doctor doctor) => Create(doctor);

        public void DeleteDoctor(Doctor doctor) => Delete(doctor);
    }
}
