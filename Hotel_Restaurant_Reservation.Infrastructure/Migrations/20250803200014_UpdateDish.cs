using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Dishes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RestaurantDishPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "RestaurantDishPrices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RestaurantDishPrices");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "RestaurantDishPrices");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Dishes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Dishes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
