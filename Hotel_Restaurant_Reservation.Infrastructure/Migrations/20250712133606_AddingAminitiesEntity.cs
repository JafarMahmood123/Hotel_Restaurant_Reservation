using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingAminitiesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyTypes_Event_EventId",
                table: "CurrencyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Locations_LocationId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistration_Customers_CustomerId",
                table: "EventRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistration_Event_EventId",
                table: "EventRegistration");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelRangePrices_HotelRangePricesId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Locations_LocationId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_PropertyTypes_PropertyTypeId",
                table: "Hotels");

            migrationBuilder.DropTable(
                name: "RoomRoomAmenity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventRegistration",
                table: "EventRegistration");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "HotelReservationId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomAmenitiesId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RoomAmenities");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "HotelReservations");

            migrationBuilder.RenameTable(
                name: "EventRegistration",
                newName: "EventRegistrations");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameIndex(
                name: "IX_EventRegistration_EventId",
                table: "EventRegistrations",
                newName: "IX_EventRegistrations_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventRegistration_CustomerId",
                table: "EventRegistrations",
                newName: "IX_EventRegistrations_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_LocationId",
                table: "Events",
                newName: "IX_Events_LocationId");

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AmenityId",
                table: "RoomAmenities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyTypeId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelReservationId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelRangePricesId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyTypeId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventRegistrations",
                table: "EventRegistrations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HotelAmenityPrice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmenityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelAmenityPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelAmenityPrice_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelAmenityPrice_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_AmenityId",
                table: "RoomAmenities",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_RoomId",
                table: "RoomAmenities",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelAmenityPrice_AmenityId",
                table: "HotelAmenityPrice",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelAmenityPrice_HotelId",
                table: "HotelAmenityPrice",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyTypes_Events_EventId",
                table: "CurrencyTypes",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Customers_CustomerId",
                table: "EventRegistrations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Events_EventId",
                table: "EventRegistrations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_HotelRangePrices_HotelRangePricesId",
                table: "Hotels",
                column: "HotelRangePricesId",
                principalTable: "HotelRangePrices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Locations_LocationId",
                table: "Hotels",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_PropertyTypes_PropertyTypeId",
                table: "Hotels",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Amenities_AmenityId",
                table: "RoomAmenities",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenities_Rooms_RoomId",
                table: "RoomAmenities",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyTypes_Events_EventId",
                table: "CurrencyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Customers_CustomerId",
                table: "EventRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Events_EventId",
                table: "EventRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

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
                name: "FK_RoomAmenities_Amenities_AmenityId",
                table: "RoomAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenities_Rooms_RoomId",
                table: "RoomAmenities");

            migrationBuilder.DropTable(
                name: "HotelAmenityPrice");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropIndex(
                name: "IX_RoomAmenities_AmenityId",
                table: "RoomAmenities");

            migrationBuilder.DropIndex(
                name: "IX_RoomAmenities_RoomId",
                table: "RoomAmenities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventRegistrations",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AmenityId",
                table: "RoomAmenities");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameTable(
                name: "EventRegistrations",
                newName: "EventRegistration");

            migrationBuilder.RenameIndex(
                name: "IX_Events_LocationId",
                table: "Event",
                newName: "IX_Event_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_EventRegistrations_EventId",
                table: "EventRegistration",
                newName: "IX_EventRegistration_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventRegistrations_CustomerId",
                table: "EventRegistration",
                newName: "IX_EventRegistration_CustomerId");

            migrationBuilder.AddColumn<Guid>(
                name: "HotelReservationId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RoomAmenitiesId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RoomAmenities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyTypeId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelReservationId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelRangePricesId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyTypeId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomNumber",
                table: "HotelReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventRegistration",
                table: "EventRegistration",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_RoomRoomAmenity_RoomsId",
                table: "RoomRoomAmenity",
                column: "RoomsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyTypes_Event_EventId",
                table: "CurrencyTypes",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Locations_LocationId",
                table: "Event",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistration_Customers_CustomerId",
                table: "EventRegistration",
                column: "CustomerId",
                principalTable: "Customers",
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
        }
    }
}
