using Microsoft.EntityFrameworkCore.Migrations;

namespace Mifdemo.Infrastructure.Migrations
{
    public partial class AttributeAddedToVocherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Reference",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Vouchers");
        }
    }
}
