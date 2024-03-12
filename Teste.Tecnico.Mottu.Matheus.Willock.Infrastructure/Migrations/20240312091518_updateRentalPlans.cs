using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Teste.Tecnico.Mottu.Matheus.Willock.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateRentalPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentedMotorcyles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAdmin",
                table: "UserAdmin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Motorcycle",
                table: "Motorcycle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryMan",
                table: "DeliveryMan");

            migrationBuilder.DropColumn(
                name: "MotorcycleRental",
                table: "DeliveryMan");

            migrationBuilder.RenameTable(
                name: "UserAdmin",
                newName: "UsersAdmin");

            migrationBuilder.RenameTable(
                name: "Motorcycle",
                newName: "Motorcycles");

            migrationBuilder.RenameTable(
                name: "DeliveryMan",
                newName: "DeliveryMen");

            migrationBuilder.AddColumn<Guid>(
                name: "RentalPlan",
                table: "DeliveryMen",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersAdmin",
                table: "UsersAdmin",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motorcycles",
                table: "Motorcycles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryMen",
                table: "DeliveryMen",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RentalPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentValue = table.Column<int>(type: "integer", nullable: false),
                    Days = table.Column<int>(type: "integer", nullable: false),
                    CostPerDay = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalPlans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersAdmin",
                table: "UsersAdmin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Motorcycles",
                table: "Motorcycles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryMen",
                table: "DeliveryMen");

            migrationBuilder.DropColumn(
                name: "RentalPlan",
                table: "DeliveryMen");

            migrationBuilder.RenameTable(
                name: "UsersAdmin",
                newName: "UserAdmin");

            migrationBuilder.RenameTable(
                name: "Motorcycles",
                newName: "Motorcycle");

            migrationBuilder.RenameTable(
                name: "DeliveryMen",
                newName: "DeliveryMan");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "MotorcycleRental",
                table: "DeliveryMan",
                type: "uuid[]",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAdmin",
                table: "UserAdmin",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motorcycle",
                table: "Motorcycle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryMan",
                table: "DeliveryMan",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RentedMotorcyles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsRented = table.Column<bool>(type: "boolean", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentedMotorcyles", x => x.Id);
                });
        }
    }
}
