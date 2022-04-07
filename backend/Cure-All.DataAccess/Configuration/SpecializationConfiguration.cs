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
                    Name = "Педиатр",
                    Description = "Врач, занимающийся вопросами здоровья младенцев, детей, подростков и молодых людей."
                },
                new Specialization
                {
                    Id = new Guid("ae8e0c0f-2a06-4b14-bcc2-156fbc321dce"),
                    Name = "Невролог",
                    Description = "Врач, лечащий заболевания головного и спинного мозга, периферических нервов и мышц."
                },
                new Specialization
                {
                    Id = new Guid("b7ca4092-c54a-46d1-8cf9-550b08cfd3cf"),
                    Name = "Аллерголог",
                    Description = "Врач, прошедший обучение по диагностике, лечению и лечению аллергии, астмы и иммунологических нарушений, включая первичные иммунодефицитные состояния."
                },
                new Specialization
                {
                    Id = new Guid("4a755e99-2e01-4d28-961e-05a537b34b84"),
                    Name = "Гинеколог",
                    Description = "Врач, специализирующийся на женском репродуктивном здоровье."
                },
                new Specialization
                {
                    Id = new Guid("dbe220dd-7310-4433-b40d-0ea3a8c2892e"),
                    Name = "Уролог",
                    Description = "Врач, занимающийся диагностикой и лечением заболеваний мочевыводящих путей у мужчин и женщин."
                },
                new Specialization
                {
                    Id = new Guid("cdd45304-ad8d-4029-bfd9-711c77f40bd6"),
                    Name = "Офтальмолог",
                    Description = "Врач, обученный диагностировать и лечить все проблемы с глазами и зрением, включая услуги по лечению зрения (очки и контактные линзы), а также проводить лечение и профилактику заболеваний глаз, включая хирургию."
                },
                new Specialization
                {
                    Id = new Guid("d50635ef-a10c-4497-9fd3-22bee5de9168"),
                    Name = "Психиатр",
                    Description = "Врач, специализирующийся на психическом здоровье, включая расстройства, связанные с употреблением психоактивных веществ."
                }
            );
        }
    }
}
