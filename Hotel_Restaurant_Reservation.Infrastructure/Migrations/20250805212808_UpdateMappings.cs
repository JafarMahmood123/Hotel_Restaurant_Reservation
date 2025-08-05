using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecommendationUserId",
                table: "UserMappings",
                newName: "YelpUserId");

            migrationBuilder.RenameColumn(
                name: "YelpRestaurantId",
                table: "RestaurantMappings",
                newName: "YelpBusinessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YelpUserId",
                table: "UserMappings",
                newName: "RecommendationUserId");

            migrationBuilder.RenameColumn(
                name: "YelpBusinessId",
                table: "RestaurantMappings",
                newName: "YelpRestaurantId");
        }
    }
}
