using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }

        public Guid PatientCardId { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientLastName { get; set; }

        public Guid DoctorId { get; set; }

        public Guid DoctorUserId { get; set; }

        public string DoctorFirstName { get; set; }

        public string DoctorLastName { get; set; }

        public string Description { get; set; }

        public Guid? IllnessId { get; set; }

        public string IllnessName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Completed { get; set; }
    }
}
