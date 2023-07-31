using Microsoft.EntityFrameworkCore.Migrations;

namespace Mifdemo.Infrastructure.Migrations
{
    public partial class AddRateOnPurchasedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rate",
                table: "PurchasedProducts",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "PurchasedProducts");
        }
    }
}
