using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelRangePrices_HotelRangePricesId",
                table: "Hotels");

            migrationBuilder.DropTable(
                name: "HotelRangePrices");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelRangePricesId",
                table: "Hotels");

            migrationBuilder.AddColumn<double>(
                name: "MaxPrice",
                table: "Hotels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MinPrice",
                table: "Hotels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "MinPrice",
                table: "Hotels");

            migrationBuilder.CreateTable(
                name: "HotelRangePrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxPrice = table.Column<double>(type: "float", nullable: false),
                    MinPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRangePrices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelRangePricesId",
                table: "Hotels",
                column: "HotelRangePricesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_HotelRangePrices_HotelRangePricesId",
                table: "Hotels",
                column: "HotelRangePricesId",
                principalTable: "HotelRangePrices",
                principalColumn: "Id");
        }
    }
}
