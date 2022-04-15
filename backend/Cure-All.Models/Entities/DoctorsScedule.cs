using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.Entities
{
    public class DoctorsScedule
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Doctor))]
        public Guid DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public DayOfWeek dayOfWeek { get; set; }
    }
}
