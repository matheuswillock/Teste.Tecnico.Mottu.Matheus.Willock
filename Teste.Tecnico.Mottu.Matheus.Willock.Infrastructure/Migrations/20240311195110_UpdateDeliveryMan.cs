using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeliveryMan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryMan_Documents_DocumentId",
                table: "DeliveryMan");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryMan_DocumentId",
                table: "DeliveryMan");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "DeliveryMan",
                newName: "Document");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Document",
                table: "DeliveryMan",
                newName: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryMan_DocumentId",
                table: "DeliveryMan",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryMan_Documents_DocumentId",
                table: "DeliveryMan",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
