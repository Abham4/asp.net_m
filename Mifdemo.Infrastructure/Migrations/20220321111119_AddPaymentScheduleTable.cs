using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mifdemo.Infrastructure.Migrations
{
    public partial class AddPaymentScheduleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PayingDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PricipalDue = table.Column<double>(type: "double", nullable: false),
                    LoanBalance = table.Column<double>(type: "double", nullable: false),
                    Interest = table.Column<double>(type: "double", nullable: false),
                    Penality = table.Column<double>(type: "double", nullable: false),
                    Paid = table.Column<double>(type: "double", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    PurchasedProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentSchedules_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentSchedules_PurchasedProducts_PurchasedProductId",
                        column: x => x.PurchasedProductId,
                        principalTable: "PurchasedProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedules_ClientId",
                table: "PaymentSchedules",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentSchedules_PurchasedProductId",
                table: "PaymentSchedules",
                column: "PurchasedProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentSchedules");
        }
    }
}
