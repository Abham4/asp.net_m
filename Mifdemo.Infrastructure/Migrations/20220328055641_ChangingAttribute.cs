using Microsoft.EntityFrameworkCore.Migrations;

namespace Mifdemo.Infrastructure.Migrations
{
    public partial class ChangingAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Paid",
                table: "PaymentSchedules",
                type: "double",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Paid",
                table: "PaymentSchedules",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }
    }
}
