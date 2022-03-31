using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.DataAccess.Configuration
{
    class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData
            (
                new IdentityUserRole<string>
                {
                    RoleId = "bd70f5f5-5ee3-4f84-92a9-2677f943a90e",
                    UserId = "73cdd0ca-72f5-4eab-97b1-5f08535814e5"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "4804449b-83a4-4796-8355-88f317323715",
                    UserId = "15bb0fef-2480-41ae-8b04-feedb9ee7f16"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "01453c60-8d7a-4078-a9c5-94b297b7ad97",
                    UserId = "3476e580-dc43-4425-9509-4743484780d3"
                }
            );
        }
    }
}
