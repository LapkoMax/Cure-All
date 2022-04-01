using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.Entities
{
    public class PatientIllneses
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(PatientCard))]
        public Guid PatientCardId { get; set; }

        public PatientCard PatientCard { get; set; }

        [ForeignKey(nameof(Illness))]
        public Guid IllnessId { get; set; }

        public Illness Illness { get; set; }
    }
}
