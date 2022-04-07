using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cure_All.DataAccess.Migrations
{
    public partial class Translateseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4804449b-83a4-4796-8355-88f317323715", "15bb0fef-2480-41ae-8b04-feedb9ee7f16" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01453c60-8d7a-4078-a9c5-94b297b7ad97", "3476e580-dc43-4425-9509-4743484780d3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bd70f5f5-5ee3-4f84-92a9-2677f943a90e", "73cdd0ca-72f5-4eab-97b1-5f08535814e5" });

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("7d66e4b1-32dc-43c3-a373-ac3b6115261e"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("72c8072f-22c9-42f2-a493-62f9a1d0f0d8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "15bb0fef-2480-41ae-8b04-feedb9ee7f16");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3476e580-dc43-4425-9509-4743484780d3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "73cdd0ca-72f5-4eab-97b1-5f08535814e5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01453c60-8d7a-4078-a9c5-94b297b7ad97",
                column: "ConcurrencyStamp",
                value: "35e1cfb6-59a2-4bd0-b8cf-bef6023ff82f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4804449b-83a4-4796-8355-88f317323715",
                column: "ConcurrencyStamp",
                value: "67d72270-7eeb-4777-b6c7-8ee9560a6ec3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd70f5f5-5ee3-4f84-92a9-2677f943a90e",
                column: "ConcurrencyStamp",
                value: "22d67680-7b8e-4bbc-bb7e-0aec99a6cfb3");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("4a755e99-2e01-4d28-961e-05a537b34b84"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Врач, специализирующийся на женском репродуктивном здоровье.", "Гинеколог" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("a145fa19-2c78-4400-ac0b-cb268b097ebc"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Врач, занимающийся вопросами здоровья младенцев, детей, подростков и молодых людей.", "Педиатр" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("ae8e0c0f-2a06-4b14-bcc2-156fbc321dce"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Врач, лечащий заболевания головного и спинного мозга, периферических нервов и мышц.", "Невролог" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("b7ca4092-c54a-46d1-8cf9-550b08cfd3cf"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Врач, прошедший обучение по диагностике, лечению и лечению аллергии, астмы и иммунологических нарушений, включая первичные иммунодефицитные состояния.", "Аллерголог" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("cdd45304-ad8d-4029-bfd9-711c77f40bd6"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Врач, обученный диагностировать и лечить все проблемы с глазами и зрением, включая услуги по лечению зрения (очки и контактные линзы), а также проводить лечение и профилактику заболеваний глаз, включая хирургию.", "Офтальмолог" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("d50635ef-a10c-4497-9fd3-22bee5de9168"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Врач, специализирующийся на психическом здоровье, включая расстройства, связанные с употреблением психоактивных веществ.", "Психиатр" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("dbe220dd-7310-4433-b40d-0ea3a8c2892e"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Врач, занимающийся диагностикой и лечением заболеваний мочевыводящих путей у мужчин и женщин.", "Уролог" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01453c60-8d7a-4078-a9c5-94b297b7ad97",
                column: "ConcurrencyStamp",
                value: "2977fdfa-2539-4047-9c30-f9469f5ef4c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4804449b-83a4-4796-8355-88f317323715",
                column: "ConcurrencyStamp",
                value: "945a48f4-ff4d-4934-bceb-34a98af67204");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd70f5f5-5ee3-4f84-92a9-2677f943a90e",
                column: "ConcurrencyStamp",
                value: "c5eda499-0814-4d97-bc1e-5fd68d91a32c");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "DateOfBurth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName", "ZipCode" },
                values: new object[,]
                {
                    { "73cdd0ca-72f5-4eab-97b1-5f08535814e5", 0, null, "b7088e4d-33ad-4347-851d-3c361b50ee03", null, new DateTime(2022, 4, 6, 10, 49, 2, 887, DateTimeKind.Local).AddTicks(5285), "admin@test.com", false, "Admin", "Admin", false, null, null, null, null, null, false, "b21025b6-23d1-4550-8f09-21c145a88c89", false, null, "AdminTest", null },
                    { "15bb0fef-2480-41ae-8b04-feedb9ee7f16", 0, null, "b84b93f8-2ca8-4ec2-8dd2-ff38a986f7a1", null, new DateTime(2022, 4, 6, 10, 49, 2, 888, DateTimeKind.Local).AddTicks(5560), "doctor@test.com", false, "Doctor", "Doctor", false, null, null, null, null, null, false, "4cf31924-2e2e-4f02-82cd-5c3dfa53da36", false, "Doctor", "DoctorTest", null },
                    { "3476e580-dc43-4425-9509-4743484780d3", 0, null, "cf2a8c73-42e6-4f11-ac2f-1167f8c829d6", null, new DateTime(2022, 4, 6, 10, 49, 2, 888, DateTimeKind.Local).AddTicks(6025), "patient@test.com", false, "Patient", "Patient", false, null, null, null, null, null, false, "36574e57-0c86-466a-80d6-2557918f4797", false, "Patient", "PatientTest", null }
                });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("4a755e99-2e01-4d28-961e-05a537b34b84"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Doctor who specializes in female reproductive health.", "Gynecologist" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("a145fa19-2c78-4400-ac0b-cb268b097ebc"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Doctor who focuses on the health of infants, children, adolescents and young adults.", "Pediatrician" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("ae8e0c0f-2a06-4b14-bcc2-156fbc321dce"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Doctor who treat diseases of the brain and spinal cord, peripheral nerves and muscles.", "Neurologist" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("b7ca4092-c54a-46d1-8cf9-550b08cfd3cf"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Doctor who trained to diagnose, treat and manage allergies, asthma and immunologic disorders including primary immunodeficiency disorders.", "Allergist" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("cdd45304-ad8d-4029-bfd9-711c77f40bd6"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Doctor who trained to diagnose and treat all eye and visual problems including vision services (glasses and contacts) and provide treatment and prevention of medical disorders of the eye including surgery.", "Ophthalmologist" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("d50635ef-a10c-4497-9fd3-22bee5de9168"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Doctor who specializes in mental health, including substance use disorders.", "Psychiatrist" });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("dbe220dd-7310-4433-b40d-0ea3a8c2892e"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Doctor who diagnose and treat diseases of the urinary tract in both men and women.", "Urologist" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "bd70f5f5-5ee3-4f84-92a9-2677f943a90e", "73cdd0ca-72f5-4eab-97b1-5f08535814e5" },
                    { "4804449b-83a4-4796-8355-88f317323715", "15bb0fef-2480-41ae-8b04-feedb9ee7f16" },
                    { "01453c60-8d7a-4078-a9c5-94b297b7ad97", "3476e580-dc43-4425-9509-4743484780d3" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "LicenseNo", "SpecializationId", "UserId", "WorkAddress", "WorkStart" },
                values: new object[] { new Guid("7d66e4b1-32dc-43c3-a373-ac3b6115261e"), "123456", new Guid("ae8e0c0f-2a06-4b14-bcc2-156fbc321dce"), "15bb0fef-2480-41ae-8b04-feedb9ee7f16", "TestAddress", new DateTime(2022, 4, 6, 10, 49, 2, 891, DateTimeKind.Local).AddTicks(5062) });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "UserId" },
                values: new object[] { new Guid("72c8072f-22c9-42f2-a493-62f9a1d0f0d8"), "3476e580-dc43-4425-9509-4743484780d3" });
        }
    }
}
