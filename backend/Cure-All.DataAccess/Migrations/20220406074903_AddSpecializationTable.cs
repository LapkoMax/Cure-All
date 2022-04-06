using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cure_All.DataAccess.Migrations
{
    public partial class AddSpecializationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "Doctors");

            migrationBuilder.AddColumn<Guid>(
                name: "SpecializationId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "15bb0fef-2480-41ae-8b04-feedb9ee7f16",
                columns: new[] { "ConcurrencyStamp", "DateOfBurth", "SecurityStamp" },
                values: new object[] { "b84b93f8-2ca8-4ec2-8dd2-ff38a986f7a1", new DateTime(2022, 4, 6, 10, 49, 2, 888, DateTimeKind.Local).AddTicks(5560), "4cf31924-2e2e-4f02-82cd-5c3dfa53da36" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3476e580-dc43-4425-9509-4743484780d3",
                columns: new[] { "ConcurrencyStamp", "DateOfBurth", "SecurityStamp" },
                values: new object[] { "cf2a8c73-42e6-4f11-ac2f-1167f8c829d6", new DateTime(2022, 4, 6, 10, 49, 2, 888, DateTimeKind.Local).AddTicks(6025), "36574e57-0c86-466a-80d6-2557918f4797" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "73cdd0ca-72f5-4eab-97b1-5f08535814e5",
                columns: new[] { "ConcurrencyStamp", "DateOfBurth", "SecurityStamp" },
                values: new object[] { "b7088e4d-33ad-4347-851d-3c361b50ee03", new DateTime(2022, 4, 6, 10, 49, 2, 887, DateTimeKind.Local).AddTicks(5285), "b21025b6-23d1-4550-8f09-21c145a88c89" });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("a145fa19-2c78-4400-ac0b-cb268b097ebc"), "Doctor who focuses on the health of infants, children, adolescents and young adults.", "Pediatrician" },
                    { new Guid("ae8e0c0f-2a06-4b14-bcc2-156fbc321dce"), "Doctor who treat diseases of the brain and spinal cord, peripheral nerves and muscles.", "Neurologist" },
                    { new Guid("b7ca4092-c54a-46d1-8cf9-550b08cfd3cf"), "Doctor who trained to diagnose, treat and manage allergies, asthma and immunologic disorders including primary immunodeficiency disorders.", "Allergist" },
                    { new Guid("4a755e99-2e01-4d28-961e-05a537b34b84"), "Doctor who specializes in female reproductive health.", "Gynecologist" },
                    { new Guid("dbe220dd-7310-4433-b40d-0ea3a8c2892e"), "Doctor who diagnose and treat diseases of the urinary tract in both men and women.", "Urologist" },
                    { new Guid("cdd45304-ad8d-4029-bfd9-711c77f40bd6"), "Doctor who trained to diagnose and treat all eye and visual problems including vision services (glasses and contacts) and provide treatment and prevention of medical disorders of the eye including surgery.", "Ophthalmologist" },
                    { new Guid("d50635ef-a10c-4497-9fd3-22bee5de9168"), "Doctor who specializes in mental health, including substance use disorders.", "Psychiatrist" }
                });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("7d66e4b1-32dc-43c3-a373-ac3b6115261e"),
                columns: new[] { "SpecializationId", "WorkStart" },
                values: new object[] { new Guid("ae8e0c0f-2a06-4b14-bcc2-156fbc321dce"), new DateTime(2022, 4, 6, 10, 49, 2, 891, DateTimeKind.Local).AddTicks(5062) });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01453c60-8d7a-4078-a9c5-94b297b7ad97",
                column: "ConcurrencyStamp",
                value: "a4582f09-0c99-442d-be18-ee49981cb50d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4804449b-83a4-4796-8355-88f317323715",
                column: "ConcurrencyStamp",
                value: "fda6882a-9945-4632-ae95-60d651431bca");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd70f5f5-5ee3-4f84-92a9-2677f943a90e",
                column: "ConcurrencyStamp",
                value: "2c69589e-1144-4de3-a2ff-4bf30ef27608");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "15bb0fef-2480-41ae-8b04-feedb9ee7f16",
                columns: new[] { "ConcurrencyStamp", "DateOfBurth", "SecurityStamp" },
                values: new object[] { "fd851893-05ed-432a-b427-689b94349e75", new DateTime(2022, 4, 1, 13, 17, 51, 71, DateTimeKind.Local).AddTicks(2676), "a73f592e-c38c-44a1-9226-ec3a00158d0e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3476e580-dc43-4425-9509-4743484780d3",
                columns: new[] { "ConcurrencyStamp", "DateOfBurth", "SecurityStamp" },
                values: new object[] { "18d8aa60-83ee-4921-838a-3eeb7850237c", new DateTime(2022, 4, 1, 13, 17, 51, 71, DateTimeKind.Local).AddTicks(3006), "03e32eb7-33b0-4810-ac6d-e0e9891f479a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "73cdd0ca-72f5-4eab-97b1-5f08535814e5",
                columns: new[] { "ConcurrencyStamp", "DateOfBurth", "SecurityStamp" },
                values: new object[] { "6adab0fe-14b3-4d8c-8105-928b5773e021", new DateTime(2022, 4, 1, 13, 17, 51, 70, DateTimeKind.Local).AddTicks(2815), "aa18ab81-1110-46e8-91bd-ffd8a98697ef" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("7d66e4b1-32dc-43c3-a373-ac3b6115261e"),
                columns: new[] { "Speciality", "WorkStart" },
                values: new object[] { "TestSpeciality", new DateTime(2022, 4, 1, 13, 17, 51, 72, DateTimeKind.Local).AddTicks(9605) });
        }
    }
}
