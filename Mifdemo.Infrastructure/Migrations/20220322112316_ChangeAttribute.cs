using Microsoft.EntityFrameworkCore.Migrations;

namespace Mifdemo.Infrastructure.Migrations
{
    public partial class ChangeAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Paid",
                table: "PaymentSchedules",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Paid",
                table: "PaymentSchedules",
                type: "double",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }
    }
}
