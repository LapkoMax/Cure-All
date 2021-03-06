using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class AppointmentForEditingDto
    {
        public string Description { get; set; }

        public Guid? IllnessId { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Completed { get; set; }
    }
}
