using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Editthecurrencytypetofittherestauratsonly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantCuisines_Cuisine_CuisineId",
                table: "RestaurantCuisines");

            migrationBuilder.DropTable(
                name: "CurrencyTypeEvent");

            migrationBuilder.DropTable(
                name: "CurrencyTypeHotel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuisine",
                table: "Cuisine");

            migrationBuilder.RenameTable(
                name: "Cuisine",
                newName: "Cuisines");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "CurrencyTypes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "CurrencyTypes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuisines",
                table: "Cuisines",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyTypes_EventId",
                table: "CurrencyTypes",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyTypes_HotelId",
                table: "CurrencyTypes",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyTypes_Event_EventId",
                table: "CurrencyTypes",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyTypes_Hotels_HotelId",
                table: "CurrencyTypes",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantCuisines_Cuisines_CuisineId",
                table: "RestaurantCuisines",
                column: "CuisineId",
                principalTable: "Cuisines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyTypes_Event_EventId",
                table: "CurrencyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyTypes_Hotels_HotelId",
                table: "CurrencyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantCuisines_Cuisines_CuisineId",
                table: "RestaurantCuisines");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyTypes_EventId",
                table: "CurrencyTypes");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyTypes_HotelId",
                table: "CurrencyTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cuisines",
                table: "Cuisines");

            migrationBuilder.RenameTable(
                name: "Cuisines",
                newName: "Cuisine");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "CurrencyTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "CurrencyTypes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cuisine",
                table: "Cuisine",
                column: "Id");

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
                name: "CurrencyTypeHotel",
                columns: table => new
                {
                    CurrencyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyTypeHotel", x => new { x.CurrencyTypeId, x.HotelsId });
                    table.ForeignKey(
                        name: "FK_CurrencyTypeHotel_CurrencyTypes_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyTypeHotel_Hotels_HotelsId",
                        column: x => x.HotelsId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyTypeEvent_EventsId",
                table: "CurrencyTypeEvent",
                column: "EventsId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyTypeHotel_HotelsId",
                table: "CurrencyTypeHotel",
                column: "HotelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantCuisines_Cuisine_CuisineId",
                table: "RestaurantCuisines",
                column: "CuisineId",
                principalTable: "Cuisine",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
