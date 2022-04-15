using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class DoctorDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Specialization { get; set; }

        public string LicenseNo { get; set; }

        public DateTime WorkStart { get; set; }

        public int YearsOfExperience { get; set; }

        public string WorkAddress { get; set; }

        public int AverageAppointmentTime { get; set; }

        public string WorkDayStart { get; set; }

        public string WorkDayEnd { get; set; }

        public string DinnerStart { get; set; }

        public string DinnerEnd { get; set; }

        public IEnumerable<DoctorsSceduleDto> DoctorsScedule { get; set; }

        public IEnumerable<DoctorDayOffsDto> DoctorDayOffs { get; set; }

        public DateTime DateOfBurth { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
    }
}
