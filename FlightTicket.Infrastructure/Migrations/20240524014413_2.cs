using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightTicket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Flights_FlightEntityId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_FlightEntityId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FlightEntityId",
                table: "Flights");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FlightEntityId",
                table: "Flights",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FlightEntityId",
                table: "Flights",
                column: "FlightEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Flights_FlightEntityId",
                table: "Flights",
                column: "FlightEntityId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
