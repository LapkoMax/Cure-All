using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.BusinessLogic.RequestFeatures
{
    public class DoctorParameters : RequestParameters
    {
        public DoctorParameters()
        {
            OrderBy = "lastname";
        }

        public int MinExperienceYears { get; set; }

        public string FullNameSearchTerm { get; set; }

        public string SpecialitySearchTerm { get; set; }

        public string CountrySearchTerm { get; set; }

        public string CitySearchTerm { get; set; }
    }
}
