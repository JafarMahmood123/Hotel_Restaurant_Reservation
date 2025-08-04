using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWorkTimeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantWorkTimes_WorkTimes_WorkTimeId",
                table: "RestaurantWorkTimes");

            migrationBuilder.DropTable(
                name: "WorkTimes");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantWorkTimes_WorkTimeId",
                table: "RestaurantWorkTimes");

            migrationBuilder.DropColumn(
                name: "WorkTimeId",
                table: "RestaurantWorkTimes");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "CloseHour",
                table: "RestaurantWorkTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "RestaurantWorkTimes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "OpenHour",
                table: "RestaurantWorkTimes",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseHour",
                table: "RestaurantWorkTimes");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "RestaurantWorkTimes");

            migrationBuilder.DropColumn(
                name: "OpenHour",
                table: "RestaurantWorkTimes");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkTimeId",
                table: "RestaurantWorkTimes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "WorkTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CloseHour = table.Column<TimeOnly>(type: "time", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenHour = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTimes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantWorkTimes_WorkTimeId",
                table: "RestaurantWorkTimes",
                column: "WorkTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantWorkTimes_WorkTimes_WorkTimeId",
                table: "RestaurantWorkTimes",
                column: "WorkTimeId",
                principalTable: "WorkTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
