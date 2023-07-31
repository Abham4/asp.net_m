using Microsoft.EntityFrameworkCore.Migrations;

namespace Mifdemo.Infrastructure.Migrations
{
    public partial class DocumentTableEdition2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Clients_ClientModelId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ClientModelId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ClientModelId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Documents",
                newName: "DocumentType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocumentType",
                table: "Documents",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "ClientModelId",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ClientModelId",
                table: "Documents",
                column: "ClientModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Clients_ClientModelId",
                table: "Documents",
                column: "ClientModelId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
