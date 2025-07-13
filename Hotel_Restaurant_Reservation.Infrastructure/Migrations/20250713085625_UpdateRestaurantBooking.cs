using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRestaurantBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookingDish_DishId",
                table: "BookingDish",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDish_Dishes_DishId",
                table: "BookingDish",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDish_Dishes_DishId",
                table: "BookingDish");

            migrationBuilder.DropIndex(
                name: "IX_BookingDish_DishId",
                table: "BookingDish");
        }
    }
}
