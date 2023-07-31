using Microsoft.EntityFrameworkCore.Migrations;

namespace Mifdemo.Infrastructure.Migrations
{
    public partial class DocumentTableEdition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Clients_ClientId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ClientId",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Documents",
                newName: "ObjectType");

            migrationBuilder.AddColumn<int>(
                name: "ClientModelId",
                table: "Documents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Documents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Documents",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "ObjectType",
                table: "Documents",
                newName: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ClientId",
                table: "Documents",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Clients_ClientId",
                table: "Documents",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
