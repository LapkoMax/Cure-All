﻿using Cure_All.Models.Entities;
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
                    FirsName = "Admin",
                    LastName = "Admin",
                    DateOfBurth = DateTime.Now,
                    UserName = "AdminTest",
                    Email = "admin@test.com",
                    PasswordHash = "9CBA73C31AC15D21512382CE6B21E83F8B9FDDD31196FF4F54559A8E29ADD1E3BC4038C86C9BEE7512D0D8EA72EC9480580DC677A9F172B46366ECB5198615CC" //Password123!
                },
                new User
                {
                    Id = "15bb0fef-2480-41ae-8b04-feedb9ee7f16",
                    FirsName = "Doctor",
                    LastName = "Doctor",
                    DateOfBurth = DateTime.Now,
                    Type = "Doctor",
                    UserName = "DoctorTest",
                    Email = "doctor@test.com",
                    PasswordHash = "9CBA73C31AC15D21512382CE6B21E83F8B9FDDD31196FF4F54559A8E29ADD1E3BC4038C86C9BEE7512D0D8EA72EC9480580DC677A9F172B46366ECB5198615CC" //Password123!
                },
                new User
                {
                    Id = "3476e580-dc43-4425-9509-4743484780d3",
                    FirsName = "Patient",
                    LastName = "Patient",
                    DateOfBurth = DateTime.Now,
                    Type = "Patient",
                    UserName = "PatientTest",
                    Email = "patient@test.com",
                    PasswordHash = "9CBA73C31AC15D21512382CE6B21E83F8B9FDDD31196FF4F54559A8E29ADD1E3BC4038C86C9BEE7512D0D8EA72EC9480580DC677A9F172B46366ECB5198615CC" //Password123!
                }
            );
        }
    }
}