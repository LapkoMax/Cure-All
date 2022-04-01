using Cure_All.BusinessLogic.RequestFeatures;
using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync(DoctorParameters doctorParameters, bool trackChanges = false);

        Task<Doctor> GetDoctorByDoctorIdAsync(Guid doctorId, bool trackChanges = false);

        Task<Doctor> GetDoctorByUserIdAsync(string userId, bool trackChanges = false);

        void CreateDoctor(Doctor doctor);

        void DeleteDoctor(Doctor doctor);
    }
}
