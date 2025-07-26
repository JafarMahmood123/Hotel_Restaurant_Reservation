using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifySomeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Users_CustomerId",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "RestaurantBookingPayments");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "RestaurantBookingPayments");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "EventRegistrations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventRegistrations_CustomerId",
                table: "EventRegistrations",
                newName: "IX_EventRegistrations_UserId");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Users",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "RestaurantBookingPayments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyTypeId",
                table: "RestaurantBookingPayments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "HotelReservationPayments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "EventRegistrationPayments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Users_UserId",
                table: "EventRegistrations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRegistrations_Users_UserId",
                table: "EventRegistrations");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrencyTypeId",
                table: "RestaurantBookingPayments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "EventRegistrations",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_EventRegistrations_UserId",
                table: "EventRegistrations",
                newName: "IX_EventRegistrations_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "RestaurantBookingPayments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "RestaurantBookingPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "RestaurantBookingPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "HotelReservationPayments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "EventRegistrationPayments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRegistrations_Users_CustomerId",
                table: "EventRegistrations",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
