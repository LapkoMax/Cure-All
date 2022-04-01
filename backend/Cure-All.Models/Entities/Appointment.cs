using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.Entities
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(PatientCard))]
        public Guid PatientCardId { get; set; }

        public PatientCard PatientCard { get; set; }

        [ForeignKey(nameof(Doctor))]
        public Guid DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(Illness))]
        public Guid? IllnessId { get; set; }

        public Illness Illness { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Completed { get; set; }
    }
}
