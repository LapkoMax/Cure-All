using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IDoctorDayOffRepository
    {
        Task<IEnumerable<DoctorDayOffs>> GetDoctorsDayOffsByDoctorId(Guid doctorId, bool trackChanges = false);

        Task<IEnumerable<DoctorDayOffs>> GetDoctorsDayOffsByStatus(DoctorStatusEnum status, bool trackChanges = false);

        void CreateDoctorDayOffs(DoctorDayOffs doctorDayOff);

        void DeleteDoctorDayOffs(DoctorDayOffs doctorDayOff);
    }
}
