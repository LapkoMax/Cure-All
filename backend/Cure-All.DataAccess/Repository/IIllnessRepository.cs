using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IIllnessRepository
    {
        Task<IEnumerable<Illness>> GetAllIllnesesAsync(bool trackChanges = false);

        Task<Illness> GetIllnessByIdAsync(Guid illnessId, bool trackChanges = false);

        void CreateIllness(Illness illness);

        void DeleteIllness(Illness illness);
    }
}
