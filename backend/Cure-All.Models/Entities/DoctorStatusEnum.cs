using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.Entities
{
    public enum DoctorStatusEnum
    {
        Available,

        DayOff,

        SickDay,

        Holiday,

        Vacation
    }
}
