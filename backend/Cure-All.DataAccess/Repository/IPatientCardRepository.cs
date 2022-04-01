using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IPatientCardRepository
    {
        Task<IEnumerable<PatientCard>> GetAllPatientCardsAsync(bool trackChanges = false);

        Task<PatientCard> GetPatientCardByIdAsync(Guid patientCardId, bool trackChanges = false);

        Task<PatientCard> GetPatientCardForPatientAsync(Guid patientId, bool trackChanges = false);

        void CreatePatientCardForPatient(Guid patientId, PatientCard patientCard);

        void DeletePatientCard(PatientCard patientCard);
    }
}
