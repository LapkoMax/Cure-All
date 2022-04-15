using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class AvailableAppointmentTimeDto
    {
        public Guid doctorId { get; set; }

        public string Time { get; set; }
    }
}
