using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync(bool trackChanges = false);

        Task<Patient> GetPatientByPatientIdAsync(Guid patientId, bool trackChanges = false);

        Task<Patient> GetPatientByUserIdAsync(string userId, bool trackChanges = false);

        Task<int> GetPatientAmountAsync();

        void CreatePatient(Patient patient);

        void DeletePatient(Patient patient);
    }
}
