using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_RestaurantOrder_RestaurantOrderId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_CurrencyTypes_CurrencyTypeId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomAmenities_RoomAmenityId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "PriceLevelRestaurant");

            migrationBuilder.DropTable(
                name: "RestaurantRestaurantOrder");

            migrationBuilder.DropTable(
                name: "RestaurantRestaurantRangePrices");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomAmenityId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_LocationId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Event_CurrencyTypeId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Customer_RestaurantOrderId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "RoomAmenityId",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "DayEnum",
                table: "WorkTime",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "ReceiveDate",
                table: "RestaurantOrder",
                newName: "ReceiveDateTime");

            migrationBuilder.RenameColumn(
                name: "BookingDateTime",
                table: "RestaurantOrder",
                newName: "OrderDateTime");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "PriceLevel",
                newName: "MinLevel");

            migrationBuilder.RenameColumn(
                name: "ReceiveDate",
                table: "HotelReservations",
                newName: "ReceivationStartDate");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomTypeId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomAmenitiesId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "CustomerStarRating",
                table: "Reviews",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
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

            migrationBuilder.AddColumn<int>(
                name: "MaxLevel",
                table: "PriceLevel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReceivationEndDate",
                table: "HotelReservations",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyTypeId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EventRegistrationId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RestaurantOrderId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "EventRegistrationId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "HotelReservationId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CurrencyTypeEvent",
                columns: table => new
                {
                    CurrencyTypesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyTypeEvent", x => new { x.CurrencyTypesId, x.EventsId });
                    table.ForeignKey(
                        name: "FK_CurrencyTypeEvent_CurrencyTypes_CurrencyTypesId",
                        column: x => x.CurrencyTypesId,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyTypeEvent_Event_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegistrationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRegistration_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventRegistration_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomRoomAmenity",
                columns: table => new
                {
                    RoomAmenitiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomRoomAmenity", x => new { x.RoomAmenitiesId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_RoomRoomAmenity_RoomAmenities_RoomAmenitiesId",
                        column: x => x.RoomAmenitiesId,
                        principalTable: "RoomAmenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomRoomAmenity_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRole",
                columns: table => new
                {
                    CustomersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRole", x => new { x.CustomersId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_CustomerRole_Customer_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantOrder_CustomerId",
                table: "RestaurantOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantOrder_RestaurantId",
                table: "RestaurantOrder",
                column: "RestaurantId");

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
                name: "IX_Hotels_LocationId",
                table: "Hotels",
                column: "LocationId",
                unique: true);

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
                name: "IX_CurrencyTypeEvent_EventsId",
                table: "CurrencyTypeEvent",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRole_RolesId",
                table: "CustomerRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_CustomerId",
                table: "EventRegistration",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistration_EventId",
                table: "EventRegistration",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomRoomAmenity_RoomsId",
                table: "RoomRoomAmenity",
                column: "RoomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Locations_LocationId",
                table: "Customer",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Locations_LocationId",
                table: "Event",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurant_Locations_LocationId",
                table: "Restaurant",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

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
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Locations_LocationId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Locations_LocationId",
                table: "Event");

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
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "CurrencyTypeEvent");

            migrationBuilder.DropTable(
                name: "CustomerRole");

            migrationBuilder.DropTable(
                name: "EventRegistration");

            migrationBuilder.DropTable(
                name: "RoomRoomAmenity");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantOrder_CustomerId",
                table: "RestaurantOrder");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantOrder_RestaurantId",
                table: "RestaurantOrder");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_LocationId",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_PriceLevelId",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Restaurant_RangePriceId",
                table: "Restaurant");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_LocationId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Event_LocationId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Customer_LocationId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "RoomAmenitiesId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CustomerStarRating",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "RangePriceId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "RestaurantOrderId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "MaxLevel",
                table: "PriceLevel");

            migrationBuilder.DropColumn(
                name: "ReceivationEndDate",
                table: "HotelReservations");

            migrationBuilder.DropColumn(
                name: "EventRegistrationId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "EventRegistrationId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "HotelReservationId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "WorkTime",
                newName: "DayEnum");

            migrationBuilder.RenameColumn(
                name: "ReceiveDateTime",
                table: "RestaurantOrder",
                newName: "ReceiveDate");

            migrationBuilder.RenameColumn(
                name: "OrderDateTime",
                table: "RestaurantOrder",
                newName: "BookingDateTime");

            migrationBuilder.RenameColumn(
                name: "MinLevel",
                table: "PriceLevel",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "ReceivationStartDate",
                table: "HotelReservations",
                newName: "ReceiveDate");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomTypeId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomAmenityId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyTypeId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "RestaurantOrderId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "PriceLevelRestaurant",
                columns: table => new
                {
                    PriceLevelsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLevelRestaurant", x => new { x.PriceLevelsId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_PriceLevelRestaurant_PriceLevel_PriceLevelsId",
                        column: x => x.PriceLevelsId,
                        principalTable: "PriceLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceLevelRestaurant_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantRestaurantOrder",
                columns: table => new
                {
                    RestaurantOrdersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantRestaurantOrder", x => new { x.RestaurantOrdersId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_RestaurantRestaurantOrder_RestaurantOrder_RestaurantOrdersId",
                        column: x => x.RestaurantOrdersId,
                        principalTable: "RestaurantOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantRestaurantOrder_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantRestaurantRangePrices",
                columns: table => new
                {
                    RangePricesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantRestaurantRangePrices", x => new { x.RangePricesId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_RestaurantRestaurantRangePrices_RestaurantRangePrices_RangePricesId",
                        column: x => x.RangePricesId,
                        principalTable: "RestaurantRangePrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantRestaurantRangePrices_Restaurant_RestaurantsId",
                        column: x => x.RestaurantsId,
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomAmenityId",
                table: "Rooms",
                column: "RoomAmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_LocationId",
                table: "Hotels",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CurrencyTypeId",
                table: "Event",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_RestaurantOrderId",
                table: "Customer",
                column: "RestaurantOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceLevelRestaurant_RestaurantsId",
                table: "PriceLevelRestaurant",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantRestaurantOrder_RestaurantsId",
                table: "RestaurantRestaurantOrder",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantRestaurantRangePrices_RestaurantsId",
                table: "RestaurantRestaurantRangePrices",
                column: "RestaurantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_RestaurantOrder_RestaurantOrderId",
                table: "Customer",
                column: "RestaurantOrderId",
                principalTable: "RestaurantOrder",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_CurrencyTypes_CurrencyTypeId",
                table: "Event",
                column: "CurrencyTypeId",
                principalTable: "CurrencyTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomAmenities_RoomAmenityId",
                table: "Rooms",
                column: "RoomAmenityId",
                principalTable: "RoomAmenities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomTypes_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id");
        }
    }
}
