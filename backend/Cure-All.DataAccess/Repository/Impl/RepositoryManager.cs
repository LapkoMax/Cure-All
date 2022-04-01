using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository.Impl
{
    class RepositoryManager : IRepositoryManager
    {
        private DataContext _dataContext;

        private IDoctorRepository _doctorRepository;

        private IPatientRepository _patientRepository;

        private IPatientCardRepository _patientCardRepository;

        private IIllnessRepository _illnessRepository;

        private IPatientIllnesesRepository _patientIllnesesRepository;

        private IAppointmentRepository _appointmentRepository;

        public IDoctorRepository Doctor 
        {
            get
            {
                if (_doctorRepository == null)
                    _doctorRepository = new DoctorRepository(_dataContext);
                return _doctorRepository;
            }
        }

        public IPatientRepository Patient
        {
            get
            {
                if (_patientRepository == null)
                    _patientRepository = new PatientRepository(_dataContext);
                return _patientRepository;
            }
        }

        public IPatientCardRepository PatientCard
        {
            get
            {
                if (_patientCardRepository == null)
                    _patientCardRepository = new PatientCardRepository(_dataContext);
                return _patientCardRepository;
            }
        }

        public IIllnessRepository Illness
        {
            get
            {
                if (_illnessRepository == null)
                    _illnessRepository = new IllnessRepository(_dataContext);
                return _illnessRepository;
            }
        }

        public IPatientIllnesesRepository PatientIllneses
        {
            get
            {
                if (_patientIllnesesRepository == null)
                    _patientIllnesesRepository = new PatientIllnesesRepository(_dataContext);
                return _patientIllnesesRepository;
            }
        }

        public IAppointmentRepository Appointment
        {
            get
            {
                if (_appointmentRepository == null)
                    _appointmentRepository = new AppointmentRepository(_dataContext);
                return _appointmentRepository;
            }
        }

        public async Task SaveAsync() => await _dataContext.SaveChangesAsync();
    }
}
