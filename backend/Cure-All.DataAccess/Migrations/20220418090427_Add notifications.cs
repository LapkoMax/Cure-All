using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cure_All.DataAccess.Migrations
{
    public partial class Addnotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Readed = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01453c60-8d7a-4078-a9c5-94b297b7ad97",
                column: "ConcurrencyStamp",
                value: "7c544714-8df4-4db1-8032-82e440e34276");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4804449b-83a4-4796-8355-88f317323715",
                column: "ConcurrencyStamp",
                value: "6185186d-64f0-4503-ade0-66aab8b01c2b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd70f5f5-5ee3-4f84-92a9-2677f943a90e",
                column: "ConcurrencyStamp",
                value: "6e136d65-1f94-4063-a55f-adc0315770a4");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01453c60-8d7a-4078-a9c5-94b297b7ad97",
                column: "ConcurrencyStamp",
                value: "7c339c97-240d-40b3-b514-438ae4b1609a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4804449b-83a4-4796-8355-88f317323715",
                column: "ConcurrencyStamp",
                value: "3ee1afef-a646-4782-bcf1-a15ec0b4467b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd70f5f5-5ee3-4f84-92a9-2677f943a90e",
                column: "ConcurrencyStamp",
                value: "49690c74-92ea-41c4-8b59-c3dd9e9ceb1d");
        }
    }
}
