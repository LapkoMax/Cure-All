using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class PatientCardDto
    {
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }

        public string PatientUserId { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientLastName { get; set; }
    }
}
