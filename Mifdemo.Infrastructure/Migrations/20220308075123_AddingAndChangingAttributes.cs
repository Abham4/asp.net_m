using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mifdemo.Infrastructure.Migrations
{
    public partial class AddingAndChangingAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Families",
                newName: "DOB");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Clients",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "DOB",
                table: "Families",
                newName: "DateOfBirth");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Families",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
