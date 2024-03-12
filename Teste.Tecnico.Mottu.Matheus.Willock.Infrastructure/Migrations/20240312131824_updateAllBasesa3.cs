using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateAllBasesa3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RaceValue = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderIsDelivered = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
