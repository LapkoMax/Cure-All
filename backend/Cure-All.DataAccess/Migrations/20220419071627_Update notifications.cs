using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cure_All.DataAccess.Migrations
{
    public partial class Updatenotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShowFrom",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01453c60-8d7a-4078-a9c5-94b297b7ad97",
                column: "ConcurrencyStamp",
                value: "38f7a459-a09d-4737-bcd7-6dfdd5d9b27f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4804449b-83a4-4796-8355-88f317323715",
                column: "ConcurrencyStamp",
                value: "5dee0c8a-bbf8-4f1b-80b1-5b1006e01846");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd70f5f5-5ee3-4f84-92a9-2677f943a90e",
                column: "ConcurrencyStamp",
                value: "e6cedb9f-d004-4250-9142-d628d955562a");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AppointmentId",
                table: "Notifications",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Appointments_AppointmentId",
                table: "Notifications",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Appointments_AppointmentId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AppointmentId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ShowFrom",
                table: "Notifications");

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
        }
    }
}
