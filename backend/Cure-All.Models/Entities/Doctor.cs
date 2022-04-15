using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.Entities
{
    public class Doctor
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Specialization))]
        public Guid SpecializationId { get; set; }

        public Specialization Specialization { get; set; }

        public string LicenseNo { get; set; }

        public DateTime WorkStart { get; set; }

        public string WorkAddress { get; set; }

        public int AverageAppointmentTime { get; set; }

        public TimeSpan WorkDayStart { get; set; }

        public TimeSpan WorkDayEnd { get; set; }

        public TimeSpan DinnerStart { get; set; }

        public TimeSpan DinnerEnd { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<DoctorsScedule> DoctorsScedule { get; set; }

        public ICollection<DoctorDayOffs> DoctorDayOffs { get; set; }
    }
}
