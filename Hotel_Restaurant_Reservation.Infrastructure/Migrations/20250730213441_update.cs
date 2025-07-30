using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventManagers_EventManagerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelManagers_HotelManagerId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantManagers_RestaurantManagerId",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "EventManagers");

            migrationBuilder.DropTable(
                name: "HotelManagers");

            migrationBuilder.DropTable(
                name: "RestaurantManagers");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_RestaurantManagerId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelManagerId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventManagerId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RestaurantManagerId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "HotelManagerId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "EventManagerId",
                table: "Events");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantManagerId",
                table: "Restaurants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HotelManagerId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EventManagerId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelManagers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantManagers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantManagers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_RestaurantManagerId",
                table: "Restaurants",
                column: "RestaurantManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelManagerId",
                table: "Hotels",
                column: "HotelManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventManagerId",
                table: "Events",
                column: "EventManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventManagers_EventManagerId",
                table: "Events",
                column: "EventManagerId",
                principalTable: "EventManagers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_HotelManagers_HotelManagerId",
                table: "Hotels",
                column: "HotelManagerId",
                principalTable: "HotelManagers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantManagers_RestaurantManagerId",
                table: "Restaurants",
                column: "RestaurantManagerId",
                principalTable: "RestaurantManagers",
                principalColumn: "Id");
        }
    }
}
