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
    class DoctorsConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasData
            (
                new Doctor
                {
                    Id = new Guid("7d66e4b1-32dc-43c3-a373-ac3b6115261e"),
                    Speciality = "TestSpeciality",
                    LicenseNo = "123456",
                    WorkStart = DateTime.Now,
                    WorkAddress = "TestAddress",
                    UserId = "15bb0fef-2480-41ae-8b04-feedb9ee7f16"
                }
            );
        }
    }
}
