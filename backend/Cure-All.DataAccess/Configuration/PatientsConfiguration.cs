using Cure_All.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Configuration
{
    class PatientsConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            //builder.HasData
            //(
            //    new Patient
            //    {
            //        Id = new Guid("72c8072f-22c9-42f2-a493-62f9a1d0f0d8"),
            //        UserId = "3476e580-dc43-4425-9509-4743484780d3"
            //    }
            //);
        }
    }
}
