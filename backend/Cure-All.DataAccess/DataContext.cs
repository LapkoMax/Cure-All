using Cure_All.DataAccess.Configuration;
using Cure_All.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UsersConfiguration());
            builder.ApplyConfiguration(new RolesConfiguration());
            builder.ApplyConfiguration(new UserRolesConfiguration());
            builder.ApplyConfiguration(new SpecializationConfiguration());
            builder.ApplyConfiguration(new DoctorsConfiguration());
            builder.ApplyConfiguration(new PatientsConfiguration());
        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<DoctorsScedule> DoctorsScedules { get; set; }

        public DbSet<DoctorDayOffs> DoctorDayOffs { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Illness> Illnesses { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<PatientCard> PatientCards { get; set; }

        public DbSet<PatientIllneses> PatientIllneses { get; set; }
    }
}
