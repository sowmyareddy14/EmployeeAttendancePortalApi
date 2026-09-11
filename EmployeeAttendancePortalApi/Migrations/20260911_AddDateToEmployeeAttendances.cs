using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeAttendancePortalApi.Migrations
{
    public partial class AddDateToEmployeeAttendances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "EmployeeAttendances",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(2000,1,1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "EmployeeAttendances");
        }
    }
}
