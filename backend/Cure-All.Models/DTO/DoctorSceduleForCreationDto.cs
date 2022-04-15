using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class DoctorSceduleForCreationDto
    {
        public Guid DoctorId { get; set; }

        public string dayOfWeek { get; set; }
    }
}
