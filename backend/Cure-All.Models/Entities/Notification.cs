using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.Entities
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(Appointment))]
        public Guid? AppointmentId { get; set; }

        public Appointment Appointment { get; set; }

        public DateTime ShowFrom { get; set; }

        public bool Readed { get; set; }

        public string Message { get; set; }
    }
}
