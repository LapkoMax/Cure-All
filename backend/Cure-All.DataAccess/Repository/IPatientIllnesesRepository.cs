using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IPatientIllnesesRepository
    {
        Task<IEnumerable<Illness>> GetAllPatientIllnesesAsync(Guid patientCardId, bool trackChanges = false);

        void AddIllnessToPatientCard(PatientIllneses patientIllneses);

        void DeleteIllnessFromPatientCard(PatientIllneses patientIllneses);
    }
}
