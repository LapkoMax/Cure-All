using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface ISpecializationRepository
    {
        Task<IEnumerable<Specialization>> GetAllSpecializationsAsync(bool trackChanges = false);

        Task<Specialization> GetSpecializationIdAsync(Guid specializationId, bool trackChanges = false);

        void CreateSpecialization(Specialization specialization);

        void DeleteSpecialization(Specialization specialization);
    }
}
