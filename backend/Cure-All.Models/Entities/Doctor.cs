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

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
