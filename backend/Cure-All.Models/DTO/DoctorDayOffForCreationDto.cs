using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class DoctorDayOffForCreationDto
    {
        public Guid DoctorId { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }
    }
}
