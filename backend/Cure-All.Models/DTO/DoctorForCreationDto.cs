using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class DoctorForCreationDto : UserRegistrationDto
    {
        public Guid SpecializationId { get; set; }

        public string LicenseNo { get; set; }

        public DateTime WorkStart { get; set; }

        public string WorkAddress { get; set; }

        public int AverageAppointmentTime { get; set; }

        public string WorkDayStart { get; set; }

        public string WorkDayEnd { get; set; }

        public string DinnerStart { get; set; }

        public string DinnerEnd { get; set; }

        public IEnumerable<DoctorSceduleForCreationDto> DoctorsScedule { get; set; }

        public IEnumerable<DoctorDayOffForCreationDto> DoctorDayOffs { get; set; }
    }
}
