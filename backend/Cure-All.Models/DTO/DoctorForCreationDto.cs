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
    }
}
