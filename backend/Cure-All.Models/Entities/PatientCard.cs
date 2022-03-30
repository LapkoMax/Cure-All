using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.Entities
{
    public class PatientCard
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Patient))]
        public Guid PatientId { get; set; }

        public Patient Patient {get; set;}
    }
}
