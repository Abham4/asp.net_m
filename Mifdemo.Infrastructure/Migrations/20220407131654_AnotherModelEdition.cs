using Microsoft.EntityFrameworkCore.Migrations;

namespace Mifdemo.Infrastructure.Migrations
{
    public partial class AnotherModelEdition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherBranch");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Vouchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_BranchId",
                table: "Vouchers",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Branches_BranchId",
                table: "Vouchers",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Branches_BranchId",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Vouchers_BranchId",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Vouchers");

            migrationBuilder.CreateTable(
                name: "VoucherBranch",
                columns: table => new
                {
                    VoucherId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherBranch", x => new { x.VoucherId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_VoucherBranch_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherBranch_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherBranch_BranchId",
                table: "VoucherBranch",
                column: "BranchId");
        }
    }
}
