using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRestaurantBookingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDish_RestaurantBooking_RestaurantBookingId",
                table: "BookingDish");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantBooking_Customers_CustomerId",
                table: "RestaurantBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantBooking_Restaurants_RestaurantId",
                table: "RestaurantBooking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantBooking",
                table: "RestaurantBooking");

            migrationBuilder.RenameTable(
                name: "RestaurantBooking",
                newName: "RestaurantBookings");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantBooking_RestaurantId",
                table: "RestaurantBookings",
                newName: "IX_RestaurantBookings_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantBooking_CustomerId",
                table: "RestaurantBookings",
                newName: "IX_RestaurantBookings_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantBookings",
                table: "RestaurantBookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDish_RestaurantBookings_RestaurantBookingId",
                table: "BookingDish",
                column: "RestaurantBookingId",
                principalTable: "RestaurantBookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantBookings_Customers_CustomerId",
                table: "RestaurantBookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantBookings_Restaurants_RestaurantId",
                table: "RestaurantBookings",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDish_RestaurantBookings_RestaurantBookingId",
                table: "BookingDish");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantBookings_Customers_CustomerId",
                table: "RestaurantBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantBookings_Restaurants_RestaurantId",
                table: "RestaurantBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantBookings",
                table: "RestaurantBookings");

            migrationBuilder.RenameTable(
                name: "RestaurantBookings",
                newName: "RestaurantBooking");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantBookings_RestaurantId",
                table: "RestaurantBooking",
                newName: "IX_RestaurantBooking_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantBookings_CustomerId",
                table: "RestaurantBooking",
                newName: "IX_RestaurantBooking_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantBooking",
                table: "RestaurantBooking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDish_RestaurantBooking_RestaurantBookingId",
                table: "BookingDish",
                column: "RestaurantBookingId",
                principalTable: "RestaurantBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantBooking_Customers_CustomerId",
                table: "RestaurantBooking",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantBooking_Restaurants_RestaurantId",
                table: "RestaurantBooking",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
