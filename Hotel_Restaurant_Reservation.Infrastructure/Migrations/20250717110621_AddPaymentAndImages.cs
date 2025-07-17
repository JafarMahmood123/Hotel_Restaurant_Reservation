using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentAndImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventCurrencyTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCurrencyTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventCurrencyTypes_CurrencyTypes_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCurrencyTypes_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventImages_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventRegistrationPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventRegistrationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistrationPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventRegistrationPayments_EventRegistrations_EventRegistrationId",
                        column: x => x.EventRegistrationId,
                        principalTable: "EventRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelImages_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelReservationPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelReservationPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelReservationPayments_HotelReservations_HotelReservationId",
                        column: x => x.HotelReservationId,
                        principalTable: "HotelReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantBookingPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantBookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantBookingPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantBookingPayments_RestaurantBookings_RestaurantBookingId",
                        column: x => x.RestaurantBookingId,
                        principalTable: "RestaurantBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantImages_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventCurrencyTypes_CurrencyTypeId",
                table: "EventCurrencyTypes",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventCurrencyTypes_EventId",
                table: "EventCurrencyTypes",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventImages_EventId",
                table: "EventImages",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrationPayments_EventRegistrationId",
                table: "EventRegistrationPayments",
                column: "EventRegistrationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelId",
                table: "HotelImages",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReservationPayments_HotelReservationId",
                table: "HotelReservationPayments",
                column: "HotelReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantBookingPayments_RestaurantBookingId",
                table: "RestaurantBookingPayments",
                column: "RestaurantBookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantImages_RestaurantId",
                table: "RestaurantImages",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventCurrencyTypes");

            migrationBuilder.DropTable(
                name: "EventImages");

            migrationBuilder.DropTable(
                name: "EventRegistrationPayments");

            migrationBuilder.DropTable(
                name: "HotelImages");

            migrationBuilder.DropTable(
                name: "HotelReservationPayments");

            migrationBuilder.DropTable(
                name: "RestaurantBookingPayments");

            migrationBuilder.DropTable(
                name: "RestaurantImages");

            migrationBuilder.DropTable(
                name: "UserImages");
        }
    }
}
