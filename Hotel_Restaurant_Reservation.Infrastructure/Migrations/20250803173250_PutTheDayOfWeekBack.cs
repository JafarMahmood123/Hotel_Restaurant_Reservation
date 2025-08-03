using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PutTheDayOfWeekBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Day",
                table: "WorkTimes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "WorkTimes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
