using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class PatientForEditingDto : UserForEditingDto
    {
        public Guid Id { get; set; }
    }
}
