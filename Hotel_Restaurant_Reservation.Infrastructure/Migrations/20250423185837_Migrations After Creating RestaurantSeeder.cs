using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsAfterCreatingRestaurantSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Locations_LocationId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Locations_LocationId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistration_Customer_CustomerId",
                table: "EventRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistration_Event_EventId",
                table: "EventRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Customer_CustomerId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Rooms_RoomId",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelRangePrices_HotelRangePricesId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Locations_LocationId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_PropertyTypes_PropertyTypeId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_Locations_LocationId",
                table: "Restaurant");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_PriceLevel_PriceLevelId",
                table: "Restaurant");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurant_RestaurantRangePrices_RangePriceId",
                table: "Restaurant");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantOrder_Customer_CustomerId",
                table: "RestaurantOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantOrder_Restaurant_RestaurantId",
                table: "RestaurantOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customer_CustomerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Hotels_HotelId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Restaurant_RestaurantId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "CuisineRestaurant");

            migrationBuilder.DropTable(
                name: "DishRestaurant");

            migrationBuilder.DropTable(
                name: "FeatureRestaurant");

            migrationBuilder.DropTable(
                name: "MealTypeRestaurant");

            migrationBuilder.DropTable(
                name: "RestaurantTag");

            migrationBuilder.DropTable(
                name: "RestaurantWorkTime");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_HotelId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RestaurantId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantOrder_CustomerId",
                table: "RestaurantOrder");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantOrder_RestaurantId",
                table: "RestaurantOrder");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelRangePricesId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_LocationId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_PropertyTypeId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_HotelReservations_CustomerId",
                table: "HotelReservations");

            migrationBuilder.DropIndex(
                name: "IX_HotelReservations_HotelId",
                table: "HotelReservations");

            migrationBuilder.DropIndex(
                name: "IX_HotelReservations_RoomId",
                table: "HotelReservations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistration_CustomerId",
                table: "EventRegistration");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistration_EventId",
                table: "EventRegistration");

            migrationBuilder.DropIndex(
                name: "IX_Event_LocationId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Customer_LocationId",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkTime",
                table: "WorkTime");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurant",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_LocationId",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_PriceLevelId",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_RangePriceId",
                table: "Restaurant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceLevel",
                table: "PriceLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealType",
                table: "MealType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feature",
                table: "Feature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dish",
                table: "Dish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuisine",
                table: "Cuisine");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "LocalLocations");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CuisineId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "DishId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "PriceLevelId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "RangePriceId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "RangePricesId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "RestaurantOrderId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "WorkTimeId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "MaxLevel",
                table: "PriceLevel");

            migrationBuilder.DropColumn(
                name: "MinLevel",
                table: "PriceLevel");

            migrationBuilder.RenameTable(
                name: "WorkTime",
                newName: "WorkTimes");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "Restaurant",
                newName: "Restaurants");

            migrationBuilder.RenameTable(
                name: "PriceLevel",
                newName: "PriceLevels");

            migrationBuilder.RenameTable(
                name: "MealType",
                newName: "MealTypes");

            migrationBuilder.RenameTable(
                name: "Feature",
                newName: "Features");

            migrationBuilder.RenameTable(
                name: "Dish",
                newName: "Dishes");

            migrationBuilder.RenameTable(
                name: "Cuisine",
                newName: "Cuisines");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "RoomTypeId",
                table: "Rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "RoomAmenitiesId",
                table: "Rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "HotelReservationId",
                table: "Rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "HotelId1",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoomTypeId1",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomAmenities",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Role",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "HotelId1",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId1",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "RestaurantOrder",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "RestaurantOrder",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomersId",
                table: "RestaurantOrder",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId1",
                table: "RestaurantOrder",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "PropertyTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "Hotels",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "Hotels",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyTypeId",
                table: "Hotels",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Hotels",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "HotelReservationId",
                table: "Hotels",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "HotelRangePricesId",
                table: "Hotels",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyTypeId",
                table: "Hotels",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "HotelRangePricesId1",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId1",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PropertyTypeId1",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "HotelReservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "HotelReservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "HotelReservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "HotelReservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "HotelId1",
                table: "HotelReservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId1",
                table: "HotelReservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "HotelRangePrices",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "EventRegistration",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "EventRegistration",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId1",
                table: "EventRegistration",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EventId1",
                table: "EventRegistration",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Event",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "EventRegistrationId",
                table: "Event",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyTypeId",
                table: "Event",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId1",
                table: "Event",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Customer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "Customer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantOrderId",
                table: "Customer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Customer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "HotelReservationId",
                table: "Customer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "EventRegistrationId",
                table: "Customer",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId1",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "CurrencyTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "CurrencyTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTables",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "PriceLevels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Dishes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkTimes",
                table: "WorkTimes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceLevels",
                table: "PriceLevels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealTypes",
                table: "MealTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Features",
                table: "Features",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dishes",
                table: "Dishes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuisines",
                table: "Cuisines",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId1",
                table: "Rooms",
                column: "HotelId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId1",
                table: "Rooms",
                column: "RoomTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId1",
                table: "Reviews",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HotelId1",
                table: "Reviews",
                column: "HotelId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RestaurantId1",
                table: "Reviews",
                column: "RestaurantId1");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantRangePrices_RestaurantId",
                table: "RestaurantRangePrices",
                column: "RestaurantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantOrder_CustomersId",
                table: "RestaurantOrder",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantOrder_RestaurantId1",
                table: "RestaurantOrder",
                column: "RestaurantId1");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelRangePricesId1",
                table: "Hotels",
                column: "HotelRangePricesId1");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_LocationId1",
                table: "Hotels",
                column: "LocationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_PropertyTypeId1",
                table: "Hotels",
                column: "PropertyTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReservations_CustomerId1",
                table: "HotelReservations",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReservations_HotelId1",
                table: "HotelReservations",
                column: "HotelId1");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReservations_RoomId1",
                table: "HotelReservations",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_CustomerId1",
                table: "EventRegistration",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_EventId1",
                table: "EventRegistration",
                column: "EventId1");

            migrationBuilder.CreateIndex(
                name: "IX_Event_LocationId1",
                table: "Event",
                column: "LocationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LocationId1",
                table: "Customer",
                column: "LocationId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTimes_RestaurantId",
                table: "WorkTimes",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_RestaurantId",
                table: "Tags",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_LocationId",
                table: "Restaurants",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLevels_RestaurantId",
                table: "PriceLevels",
                column: "RestaurantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealTypes_RestaurantId",
                table: "MealTypes",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_RestaurantId",
                table: "Features",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_RestaurantId",
                table: "Dishes",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuisines_RestaurantId",
                table: "Cuisines",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuisines_Restaurants_RestaurantId",
                table: "Cuisines",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Locations_LocationId1",
                table: "Customer",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantId",
                table: "Dishes",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Locations_LocationId1",
                table: "Event",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistration_Customer_CustomerId1",
                table: "EventRegistration",
                column: "CustomerId1",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistration_Event_EventId1",
                table: "EventRegistration",
                column: "EventId1",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Restaurants_RestaurantId",
                table: "Features",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Customer_CustomerId1",
                table: "HotelReservations",
                column: "CustomerId1",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId1",
                table: "HotelReservations",
                column: "HotelId1",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Rooms_RoomId1",
                table: "HotelReservations",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_HotelRangePrices_HotelRangePricesId1",
                table: "Hotels",
                column: "HotelRangePricesId1",
                principalTable: "HotelRangePrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Locations_LocationId1",
                table: "Hotels",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_PropertyTypes_PropertyTypeId1",
                table: "Hotels",
                column: "PropertyTypeId1",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealTypes_Restaurants_RestaurantId",
                table: "MealTypes",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceLevels_Restaurants_RestaurantId",
                table: "PriceLevels",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantOrder_Customer_CustomersId",
                table: "RestaurantOrder",
                column: "CustomersId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantOrder_Restaurants_RestaurantId1",
                table: "RestaurantOrder",
                column: "RestaurantId1",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantRangePrices_Restaurants_RestaurantId",
                table: "RestaurantRangePrices",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Locations_LocationId",
                table: "Restaurants",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customer_CustomerId1",
                table: "Reviews",
                column: "CustomerId1",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Hotels_HotelId1",
                table: "Reviews",
                column: "HotelId1",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantId1",
                table: "Reviews",
                column: "RestaurantId1",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelId1",
                table: "Rooms",
                column: "HotelId1",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId1",
                table: "Rooms",
                column: "RoomTypeId1",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Restaurants_RestaurantId",
                table: "Tags",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkTimes_Restaurants_RestaurantId",
                table: "WorkTimes",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuisines_Restaurants_RestaurantId",
                table: "Cuisines");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Locations_LocationId1",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurants_RestaurantId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Locations_LocationId1",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistration_Customer_CustomerId1",
                table: "EventRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistration_Event_EventId1",
                table: "EventRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_Restaurants_RestaurantId",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Customer_CustomerId1",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId1",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReservations_Rooms_RoomId1",
                table: "HotelReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelRangePrices_HotelRangePricesId1",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Locations_LocationId1",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_PropertyTypes_PropertyTypeId1",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_MealTypes_Restaurants_RestaurantId",
                table: "MealTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceLevels_Restaurants_RestaurantId",
                table: "PriceLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantOrder_Customer_CustomersId",
                table: "RestaurantOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantOrder_Restaurants_RestaurantId1",
                table: "RestaurantOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantRangePrices_Restaurants_RestaurantId",
                table: "RestaurantRangePrices");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Locations_LocationId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customer_CustomerId1",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Hotels_HotelId1",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantId1",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Hotels_HotelId1",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId1",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Restaurants_RestaurantId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkTimes_Restaurants_RestaurantId",
                table: "WorkTimes");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HotelId1",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomTypeId1",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CustomerId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_HotelId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RestaurantId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantRangePrices_RestaurantId",
                table: "RestaurantRangePrices");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantOrder_CustomersId",
                table: "RestaurantOrder");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantOrder_RestaurantId1",
                table: "RestaurantOrder");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelRangePricesId1",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_LocationId1",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_PropertyTypeId1",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_HotelReservations_CustomerId1",
                table: "HotelReservations");

            migrationBuilder.DropIndex(
                name: "IX_HotelReservations_HotelId1",
                table: "HotelReservations");

            migrationBuilder.DropIndex(
                name: "IX_HotelReservations_RoomId1",
                table: "HotelReservations");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistration_CustomerId1",
                table: "EventRegistration");

            migrationBuilder.DropIndex(
                name: "IX_EventRegistration_EventId1",
                table: "EventRegistration");

            migrationBuilder.DropIndex(
                name: "IX_Event_LocationId1",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Customer_LocationId1",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkTimes",
                table: "WorkTimes");

            migrationBuilder.DropIndex(
                name: "IX_WorkTimes_RestaurantId",
                table: "WorkTimes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_RestaurantId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_LocationId",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceLevels",
                table: "PriceLevels");

            migrationBuilder.DropIndex(
                name: "IX_PriceLevels_RestaurantId",
                table: "PriceLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealTypes",
                table: "MealTypes");

            migrationBuilder.DropIndex(
                name: "IX_MealTypes_RestaurantId",
                table: "MealTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Features",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_RestaurantId",
                table: "Features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dishes",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_RestaurantId",
                table: "Dishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuisines",
                table: "Cuisines");

            migrationBuilder.DropIndex(
                name: "IX_Cuisines_RestaurantId",
                table: "Cuisines");

            migrationBuilder.DropColumn(
                name: "HotelId1",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomTypeId1",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "HotelId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RestaurantId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CustomersId",
                table: "RestaurantOrder");

            migrationBuilder.DropColumn(
                name: "RestaurantId1",
                table: "RestaurantOrder");

            migrationBuilder.DropColumn(
                name: "HotelRangePricesId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PropertyTypeId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "HotelReservations");

            migrationBuilder.DropColumn(
                name: "HotelId1",
                table: "HotelReservations");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "HotelReservations");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "EventRegistration");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "EventRegistration");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NumberOfTables",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "PriceLevels");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Dishes");

            migrationBuilder.RenameTable(
                name: "WorkTimes",
                newName: "WorkTime");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "Restaurant");

            migrationBuilder.RenameTable(
                name: "PriceLevels",
                newName: "PriceLevel");

            migrationBuilder.RenameTable(
                name: "MealTypes",
                newName: "MealType");

            migrationBuilder.RenameTable(
                name: "Features",
                newName: "Feature");

            migrationBuilder.RenameTable(
                name: "Dishes",
                newName: "Dish");

            migrationBuilder.RenameTable(
                name: "Cuisines",
                newName: "Cuisine");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "RoomTypes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomTypeId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomAmenitiesId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelReservationId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "RoomAmenities",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Role",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "RestaurantId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "RestaurantId",
                table: "RestaurantOrder",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "RestaurantOrder",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "PropertyTypes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "LocalLocations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReviewId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyTypeId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelReservationId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelRangePricesId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyTypeId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "HotelReservations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "HotelReservations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "HotelReservations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "HotelRangePrices",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "EventRegistration",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "EventRegistration",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventRegistrationId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyTypeId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReviewId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "RestaurantOrderId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelReservationId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventRegistrationId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "CurrencyTypes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "CurrencyTypes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Countries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Cities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CuisineId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DishId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FeatureId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MealTypeId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PriceLevelId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RangePriceId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RangePricesId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantOrderId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TagId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WorkTimeId",
                table: "Restaurant",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "MaxLevel",
                table: "PriceLevel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinLevel",
                table: "PriceLevel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkTime",
                table: "WorkTime",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurant",
                table: "Restaurant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceLevel",
                table: "PriceLevel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealType",
                table: "MealType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feature",
                table: "Feature",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dish",
                table: "Dish",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuisine",
                table: "Cuisine",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CuisineRestaurant",
                columns: table => new
                {
                    CuisinesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuisineRestaurant", x => new { x.CuisinesId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_CuisineRestaurant_Cuisine_CuisinesId",
                        column: x => x.CuisinesId,
                        principalTable: "Cuisine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuisineRestaurant_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishRestaurant",
                columns: table => new
                {
                    DishesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishRestaurant", x => new { x.DishesId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_DishRestaurant_Dish_DishesId",
                        column: x => x.DishesId,
                        principalTable: "Dish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishRestaurant_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeatureRestaurant",
                columns: table => new
                {
                    FeaturesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureRestaurant", x => new { x.FeaturesId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_FeatureRestaurant_Feature_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureRestaurant_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealTypeRestaurant",
                columns: table => new
                {
                    MealTypesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTypeRestaurant", x => new { x.MealTypesId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_MealTypeRestaurant_MealType_MealTypesId",
                        column: x => x.MealTypesId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealTypeRestaurant_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantTag",
                columns: table => new
                {
                    RestaurantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantTag", x => new { x.RestaurantsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_RestaurantTag_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantWorkTime",
                columns: table => new
                {
                    RestaurantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkTimesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantWorkTime", x => new { x.RestaurantsId, x.WorkTimesId });
                    table.ForeignKey(
                        name: "FK_RestaurantWorkTime_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantWorkTime_WorkTime_WorkTimesId",
                        column: x => x.WorkTimesId,
                        principalTable: "WorkTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                table: "Rooms",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HotelId",
                table: "Reviews",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RestaurantId",
                table: "Reviews",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantOrder_CustomerId",
                table: "RestaurantOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantOrder_RestaurantId",
                table: "RestaurantOrder",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelRangePricesId",
                table: "Hotels",
                column: "HotelRangePricesId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_LocationId",
                table: "Hotels",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_PropertyTypeId",
                table: "Hotels",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReservations_CustomerId",
                table: "HotelReservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReservations_HotelId",
                table: "HotelReservations",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReservations_RoomId",
                table: "HotelReservations",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_CustomerId",
                table: "EventRegistration",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_EventId",
                table: "EventRegistration",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_LocationId",
                table: "Event",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_LocationId",
                table: "Customer",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_LocationId",
                table: "Restaurant",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_PriceLevelId",
                table: "Restaurant",
                column: "PriceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurant_RangePriceId",
                table: "Restaurant",
                column: "RangePriceId");

            migrationBuilder.CreateIndex(
                name: "IX_CuisineRestaurant_RestaurantsId",
                table: "CuisineRestaurant",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_DishRestaurant_RestaurantsId",
                table: "DishRestaurant",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureRestaurant_RestaurantsId",
                table: "FeatureRestaurant",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTypeRestaurant_RestaurantsId",
                table: "MealTypeRestaurant",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantTag_TagsId",
                table: "RestaurantTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantWorkTime_WorkTimesId",
                table: "RestaurantWorkTime",
                column: "WorkTimesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Locations_LocationId",
                table: "Customer",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Locations_LocationId",
                table: "Event",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistration_Customer_CustomerId",
                table: "EventRegistration",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistration_Event_EventId",
                table: "EventRegistration",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Customer_CustomerId",
                table: "HotelReservations",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Hotels_HotelId",
                table: "HotelReservations",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReservations_Rooms_RoomId",
                table: "HotelReservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_HotelRangePrices_HotelRangePricesId",
                table: "Hotels",
                column: "HotelRangePricesId",
                principalTable: "HotelRangePrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Locations_LocationId",
                table: "Hotels",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_PropertyTypes_PropertyTypeId",
                table: "Hotels",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_Locations_LocationId",
                table: "Restaurant",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_PriceLevel_PriceLevelId",
                table: "Restaurant",
                column: "PriceLevelId",
                principalTable: "PriceLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_RestaurantRangePrices_RangePriceId",
                table: "Restaurant",
                column: "RangePriceId",
                principalTable: "RestaurantRangePrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantOrder_Customer_CustomerId",
                table: "RestaurantOrder",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantOrder_Restaurant_RestaurantId",
                table: "RestaurantOrder",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customer_CustomerId",
                table: "Reviews",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Hotels_HotelId",
                table: "Reviews",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Restaurant_RestaurantId",
                table: "Reviews",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Hotels_HotelId",
                table: "Rooms",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
