using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class NotificationDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public Guid? AppointmentId { get; set; }

        public bool Readed { get; set; }

        public string Message { get; set; }
    }
}
