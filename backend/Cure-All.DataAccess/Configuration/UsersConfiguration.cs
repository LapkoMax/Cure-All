using Cure_All.Models.DTO;
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
    class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
            (
                new User
                {
                    Id = "73cdd0ca-72f5-4eab-97b1-5f08535814e5",
                    FirstName = "Admin",
                    LastName = "Admin",
                    DateOfBurth = DateTime.Now,
                    UserName = "AdminTest",
                    Email = "admin@test.com"
                },
                new User
                {
                    Id = "15bb0fef-2480-41ae-8b04-feedb9ee7f16",
                    FirstName = "Doctor",
                    LastName = "Doctor",
                    DateOfBurth = DateTime.Now,
                    Type = "Doctor",
                    UserName = "DoctorTest",
                    Email = "doctor@test.com"
                },
                new User
                {
                    Id = "3476e580-dc43-4425-9509-4743484780d3",
                    FirstName = "Patient",
                    LastName = "Patient",
                    DateOfBurth = DateTime.Now,
                    Type = "Patient",
                    UserName = "PatientTest",
                    Email = "patient@test.com"
                }
            );
        }
    }
}
