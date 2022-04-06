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
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasData
            (
                new Specialization
                {
                    Id = new Guid("a145fa19-2c78-4400-ac0b-cb268b097ebc"),
                    Name = "Pediatrician",
                    Description = "Doctor who focuses on the health of infants, children, adolescents and young adults."
                },
                new Specialization
                {
                    Id = new Guid("ae8e0c0f-2a06-4b14-bcc2-156fbc321dce"),
                    Name = "Neurologist",
                    Description = "Doctor who treat diseases of the brain and spinal cord, peripheral nerves and muscles."
                },
                new Specialization
                {
                    Id = new Guid("b7ca4092-c54a-46d1-8cf9-550b08cfd3cf"),
                    Name = "Allergist",
                    Description = "Doctor who trained to diagnose, treat and manage allergies, asthma and immunologic disorders including primary immunodeficiency disorders."
                },
                new Specialization
                {
                    Id = new Guid("4a755e99-2e01-4d28-961e-05a537b34b84"),
                    Name = "Gynecologist",
                    Description = "Doctor who specializes in female reproductive health."
                },
                new Specialization
                {
                    Id = new Guid("dbe220dd-7310-4433-b40d-0ea3a8c2892e"),
                    Name = "Urologist",
                    Description = "Doctor who diagnose and treat diseases of the urinary tract in both men and women."
                },
                new Specialization
                {
                    Id = new Guid("cdd45304-ad8d-4029-bfd9-711c77f40bd6"),
                    Name = "Ophthalmologist",
                    Description = "Doctor who trained to diagnose and treat all eye and visual problems including vision services (glasses and contacts) and provide treatment and prevention of medical disorders of the eye including surgery."
                },
                new Specialization
                {
                    Id = new Guid("d50635ef-a10c-4497-9fd3-22bee5de9168"),
                    Name = "Psychiatrist",
                    Description = "Doctor who specializes in mental health, including substance use disorders."
                }
            );
        }
    }
}
