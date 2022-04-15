using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cure_All.DataAccess.Migrations
{
    public partial class Addstarttimeforappointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Appointments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Appointments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01453c60-8d7a-4078-a9c5-94b297b7ad97",
                column: "ConcurrencyStamp",
                value: "f39a6e6a-fb16-4bba-805d-d3252fc2f014");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4804449b-83a4-4796-8355-88f317323715",
                column: "ConcurrencyStamp",
                value: "dec2e5d5-d69f-42d9-b3e4-fcbd0d38e679");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd70f5f5-5ee3-4f84-92a9-2677f943a90e",
                column: "ConcurrencyStamp",
                value: "82493f13-89f9-45f6-b579-16e2573d8a95");
        }
    }
}
