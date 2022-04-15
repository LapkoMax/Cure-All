using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IDoctorsSceduleRepository
    {
        Task<IEnumerable<DoctorsScedule>> GetDoctorsScedule(Guid doctorId, bool trackChanges = false);

        void CreateDoctorsScedule(DoctorsScedule doctorsScedule);

        void DeleteDoctorsScedule(DoctorsScedule doctorsScedule);
    }
}
