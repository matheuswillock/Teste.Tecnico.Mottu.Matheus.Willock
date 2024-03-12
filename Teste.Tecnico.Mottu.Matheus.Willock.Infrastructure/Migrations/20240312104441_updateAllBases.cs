using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateAllBases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostPerDay",
                table: "RentalPlans");

            migrationBuilder.AlterColumn<decimal>(
                name: "RentValue",
                table: "RentalPlans",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RentValue",
                table: "RentalPlans",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerDay",
                table: "RentalPlans",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
