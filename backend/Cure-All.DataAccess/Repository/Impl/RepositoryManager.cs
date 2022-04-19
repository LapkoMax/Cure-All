using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Repository.Impl
{
    public class RepositoryManager : IRepositoryManager
    {
        private DataContext _dataContext;

        private IDoctorRepository _doctorRepository;

        private IDoctorsSceduleRepository _doctorSceduleRepository;

        private IDoctorDayOffRepository _doctorDayOffRepository;

        private IPatientRepository _patientRepository;

        private IPatientCardRepository _patientCardRepository;

        private ISpecializationRepository _specializationRepository;

        private IIllnessRepository _illnessRepository;

        private IPatientIllnesesRepository _patientIllnesesRepository;

        private IAppointmentRepository _appointmentRepository;

        private INotificationRepository _notificationRepository;

        public RepositoryManager(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IDoctorRepository Doctor 
        {
            get
            {
                if (_doctorRepository == null)
                    _doctorRepository = new DoctorRepository(_dataContext);
                return _doctorRepository;
            }
        }

        public IDoctorsSceduleRepository DoctorsScedule
        {
            get
            {
                if (_doctorSceduleRepository == null)
                    _doctorSceduleRepository = new DoctorsSceduleRepository(_dataContext);
                return _doctorSceduleRepository;
            }
        }

        public IDoctorDayOffRepository DoctorDayOff
        {
            get
            {
                if (_doctorDayOffRepository == null)
                    _doctorDayOffRepository = new DoctorDayOffRepository(_dataContext);
                return _doctorDayOffRepository;
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

        public ISpecializationRepository Specialization
        {
            get
            {
                if (_specializationRepository == null)
                    _specializationRepository = new SpecializationRepository(_dataContext);
                return _specializationRepository;
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

        public INotificationRepository Notification
        {
            get
            {
                if (_notificationRepository == null)
                    _notificationRepository = new NotificationRepository(_dataContext);
                return _notificationRepository;
            }
        }

        public async Task SaveAsync() => await _dataContext.SaveChangesAsync();
    }
}
