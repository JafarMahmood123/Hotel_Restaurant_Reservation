using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCustomerToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Locations_LocationId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Roles_RoleId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Customers_CustomerId",
                table: "EventRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReviews_Customers_CustomerId",
                table: "EventReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Customers_UserId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReviews_Customers_UserId",
                table: "HotelReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantBookings_Customers_UserId",
                table: "RestaurantBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantReviews_Customers_UserId",
                table: "RestaurantReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_RoleId",
                table: "Users",
                newName: "IX_Users_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_LocationId",
                table: "Users",
                newName: "IX_Users_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Users_CustomerId",
                table: "EventRegistrations",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventReviews_Users_CustomerId",
                table: "EventReviews",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Users_UserId",
                table: "HotelReservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReviews_Users_UserId",
                table: "HotelReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantBookings_Users_UserId",
                table: "RestaurantBookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantReviews_Users_UserId",
                table: "RestaurantReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_LocationId",
                table: "Users",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Users_CustomerId",
                table: "EventRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReviews_Users_CustomerId",
                table: "EventReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Users_UserId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReviews_Users_UserId",
                table: "HotelReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantBookings_Users_UserId",
                table: "RestaurantBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantReviews_Users_UserId",
                table: "RestaurantReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_LocationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleId",
                table: "Customers",
                newName: "IX_Customers_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LocationId",
                table: "Customers",
                newName: "IX_Customers_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Locations_LocationId",
                table: "Customers",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Roles_RoleId",
                table: "Customers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Customers_CustomerId",
                table: "EventRegistrations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventReviews_Customers_CustomerId",
                table: "EventReviews",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Customers_UserId",
                table: "HotelReservations",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReviews_Customers_UserId",
                table: "HotelReviews",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantBookings_Customers_UserId",
                table: "RestaurantBookings",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantReviews_Customers_UserId",
                table: "RestaurantReviews",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
