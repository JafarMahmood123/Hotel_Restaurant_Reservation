using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddManagers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDish_Dishes_DishId",
                table: "BookingDish");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDish_RestaurantBookings_RestaurantBookingId",
                table: "BookingDish");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReview_Customers_CustomerId",
                table: "EventReview");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReview_Events_EventId",
                table: "EventReview");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelAmenityPrice_Amenities_AmenityId",
                table: "HotelAmenityPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelAmenityPrice_Hotels_HotelId",
                table: "HotelAmenityPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelCurrencyType_CurrencyTypes_CurrencyTypeId",
                table: "HotelCurrencyType");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelCurrencyType_Hotels_HotelId",
                table: "HotelCurrencyType");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Customers_CustomerId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReview_Customers_CustomerId",
                table: "HotelReview");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReview_Hotels_HotelId",
                table: "HotelReview");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantBookings_Customers_CustomerId",
                table: "RestaurantBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantReviews_Customers_CustomerId",
                table: "RestaurantReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelReview",
                table: "HotelReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelCurrencyType",
                table: "HotelCurrencyType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelAmenityPrice",
                table: "HotelAmenityPrice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventReview",
                table: "EventReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingDish",
                table: "BookingDish");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "HotelReview",
                newName: "HotelReviews");

            migrationBuilder.RenameTable(
                name: "HotelCurrencyType",
                newName: "HotelCurrencyTypes");

            migrationBuilder.RenameTable(
                name: "HotelAmenityPrice",
                newName: "HotelAmenityPrices");

            migrationBuilder.RenameTable(
                name: "EventReview",
                newName: "EventReviews");

            migrationBuilder.RenameTable(
                name: "BookingDish",
                newName: "BookingDishes");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "RestaurantReviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantReviews_CustomerId",
                table: "RestaurantReviews",
                newName: "IX_RestaurantReviews_UserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "RestaurantBookings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantBookings_CustomerId",
                table: "RestaurantBookings",
                newName: "IX_RestaurantBookings_UserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "HotelReservations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelReservations_CustomerId",
                table: "HotelReservations",
                newName: "IX_HotelReservations_UserId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "HotelReviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelReview_HotelId",
                table: "HotelReviews",
                newName: "IX_HotelReviews_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelReview_CustomerId",
                table: "HotelReviews",
                newName: "IX_HotelReviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelCurrencyType_HotelId",
                table: "HotelCurrencyTypes",
                newName: "IX_HotelCurrencyTypes_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelCurrencyType_CurrencyTypeId",
                table: "HotelCurrencyTypes",
                newName: "IX_HotelCurrencyTypes_CurrencyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelAmenityPrice_HotelId",
                table: "HotelAmenityPrices",
                newName: "IX_HotelAmenityPrices_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelAmenityPrice_AmenityId",
                table: "HotelAmenityPrices",
                newName: "IX_HotelAmenityPrices_AmenityId");

            migrationBuilder.RenameIndex(
                name: "IX_EventReview_EventId",
                table: "EventReviews",
                newName: "IX_EventReviews_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventReview_CustomerId",
                table: "EventReviews",
                newName: "IX_EventReviews_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingDish_RestaurantBookingId",
                table: "BookingDishes",
                newName: "IX_BookingDishes_RestaurantBookingId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingDish_DishId",
                table: "BookingDishes",
                newName: "IX_BookingDishes_DishId");

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

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelReviews",
                table: "HotelReviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelCurrencyTypes",
                table: "HotelCurrencyTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelAmenityPrices",
                table: "HotelAmenityPrices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventReviews",
                table: "EventReviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingDishes",
                table: "BookingDishes",
                column: "Id");

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

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RoleId",
                table: "Customers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDishes_Dishes_DishId",
                table: "BookingDishes",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDishes_RestaurantBookings_RestaurantBookingId",
                table: "BookingDishes",
                column: "RestaurantBookingId",
                principalTable: "RestaurantBookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Roles_RoleId",
                table: "Customers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EventReviews_Customers_CustomerId",
                table: "EventReviews",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventReviews_Events_EventId",
                table: "EventReviews",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventManagers_EventManagerId",
                table: "Events",
                column: "EventManagerId",
                principalTable: "EventManagers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelAmenityPrices_Amenities_AmenityId",
                table: "HotelAmenityPrices",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelAmenityPrices_Hotels_HotelId",
                table: "HotelAmenityPrices",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelCurrencyTypes_CurrencyTypes_CurrencyTypeId",
                table: "HotelCurrencyTypes",
                column: "CurrencyTypeId",
                principalTable: "CurrencyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelCurrencyTypes_Hotels_HotelId",
                table: "HotelCurrencyTypes",
                column: "HotelId",
                principalTable: "Hotels",
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
                name: "FK_HotelReviews_Hotels_HotelId",
                table: "HotelReviews",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_HotelManagers_HotelManagerId",
                table: "Hotels",
                column: "HotelManagerId",
                principalTable: "HotelManagers",
                principalColumn: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantManagers_RestaurantManagerId",
                table: "Restaurants",
                column: "RestaurantManagerId",
                principalTable: "RestaurantManagers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDishes_Dishes_DishId",
                table: "BookingDishes");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDishes_RestaurantBookings_RestaurantBookingId",
                table: "BookingDishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Roles_RoleId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReviews_Customers_CustomerId",
                table: "EventReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_EventReviews_Events_EventId",
                table: "EventReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventManagers_EventManagerId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelAmenityPrices_Amenities_AmenityId",
                table: "HotelAmenityPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelAmenityPrices_Hotels_HotelId",
                table: "HotelAmenityPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelCurrencyTypes_CurrencyTypes_CurrencyTypeId",
                table: "HotelCurrencyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelCurrencyTypes_Hotels_HotelId",
                table: "HotelCurrencyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Customers_UserId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReviews_Customers_UserId",
                table: "HotelReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReviews_Hotels_HotelId",
                table: "HotelReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelManagers_HotelManagerId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantBookings_Customers_UserId",
                table: "RestaurantBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantReviews_Customers_UserId",
                table: "RestaurantReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantManagers_RestaurantManagerId",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "EventManagers");

            migrationBuilder.DropTable(
                name: "HotelManagers");

            migrationBuilder.DropTable(
                name: "RestaurantManagers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_RestaurantManagerId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelManagerId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventManagerId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RoleId",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelReviews",
                table: "HotelReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelCurrencyTypes",
                table: "HotelCurrencyTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelAmenityPrices",
                table: "HotelAmenityPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventReviews",
                table: "EventReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingDishes",
                table: "BookingDishes");

            migrationBuilder.DropColumn(
                name: "RestaurantManagerId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "HotelManagerId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "EventManagerId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "HotelReviews",
                newName: "HotelReview");

            migrationBuilder.RenameTable(
                name: "HotelCurrencyTypes",
                newName: "HotelCurrencyType");

            migrationBuilder.RenameTable(
                name: "HotelAmenityPrices",
                newName: "HotelAmenityPrice");

            migrationBuilder.RenameTable(
                name: "EventReviews",
                newName: "EventReview");

            migrationBuilder.RenameTable(
                name: "BookingDishes",
                newName: "BookingDish");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RestaurantReviews",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantReviews_UserId",
                table: "RestaurantReviews",
                newName: "IX_RestaurantReviews_CustomerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RestaurantBookings",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantBookings_UserId",
                table: "RestaurantBookings",
                newName: "IX_RestaurantBookings_CustomerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HotelReservations",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelReservations_UserId",
                table: "HotelReservations",
                newName: "IX_HotelReservations_CustomerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HotelReview",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelReviews_UserId",
                table: "HotelReview",
                newName: "IX_HotelReview_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelReviews_HotelId",
                table: "HotelReview",
                newName: "IX_HotelReview_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelCurrencyTypes_HotelId",
                table: "HotelCurrencyType",
                newName: "IX_HotelCurrencyType_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelCurrencyTypes_CurrencyTypeId",
                table: "HotelCurrencyType",
                newName: "IX_HotelCurrencyType_CurrencyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelAmenityPrices_HotelId",
                table: "HotelAmenityPrice",
                newName: "IX_HotelAmenityPrice_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelAmenityPrices_AmenityId",
                table: "HotelAmenityPrice",
                newName: "IX_HotelAmenityPrice_AmenityId");

            migrationBuilder.RenameIndex(
                name: "IX_EventReviews_EventId",
                table: "EventReview",
                newName: "IX_EventReview_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventReviews_CustomerId",
                table: "EventReview",
                newName: "IX_EventReview_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingDishes_RestaurantBookingId",
                table: "BookingDish",
                newName: "IX_BookingDish_RestaurantBookingId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingDishes_DishId",
                table: "BookingDish",
                newName: "IX_BookingDish_DishId");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelReview",
                table: "HotelReview",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelCurrencyType",
                table: "HotelCurrencyType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelAmenityPrice",
                table: "HotelAmenityPrice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventReview",
                table: "EventReview",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingDish",
                table: "BookingDish",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDish_Dishes_DishId",
                table: "BookingDish",
                column: "DishId",
                principalTable: "Dishes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDish_RestaurantBookings_RestaurantBookingId",
                table: "BookingDish",
                column: "RestaurantBookingId",
                principalTable: "RestaurantBookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventReview_Customers_CustomerId",
                table: "EventReview",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventReview_Events_EventId",
                table: "EventReview",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelAmenityPrice_Amenities_AmenityId",
                table: "HotelAmenityPrice",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelAmenityPrice_Hotels_HotelId",
                table: "HotelAmenityPrice",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelCurrencyType_CurrencyTypes_CurrencyTypeId",
                table: "HotelCurrencyType",
                column: "CurrencyTypeId",
                principalTable: "CurrencyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelCurrencyType_Hotels_HotelId",
                table: "HotelCurrencyType",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Customers_CustomerId",
                table: "HotelReservations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReview_Customers_CustomerId",
                table: "HotelReview",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReview_Hotels_HotelId",
                table: "HotelReview",
                column: "HotelId",
                principalTable: "Hotels",
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
                name: "FK_RestaurantReviews_Customers_CustomerId",
                table: "RestaurantReviews",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
