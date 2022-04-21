using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.BusinessLogic.RequestFeatures
{
    public class AppointmentParameters : RequestParameters
    {
        public AppointmentParameters()
        {
            OrderBy = "description";
        }

        public DateTime? Date { get; set; }
    }
}
