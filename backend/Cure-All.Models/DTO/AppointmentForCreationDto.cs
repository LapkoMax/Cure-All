using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class AppointmentForCreationDto
    {
        public Guid PatientCardId { get; set; }

        public Guid DoctorId { get; set; }

        public string Description { get; set; }

        public Guid? IllnessId { get; set; }

        public DateTime StartDate { get; set; }
    }
}
