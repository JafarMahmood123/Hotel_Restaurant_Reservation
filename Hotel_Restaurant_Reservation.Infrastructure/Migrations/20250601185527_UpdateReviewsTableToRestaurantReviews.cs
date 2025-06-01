using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReviewsTableToRestaurantReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_CustomerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "RestaurantReviews");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_RestaurantId",
                table: "RestaurantReviews",
                newName: "IX_RestaurantReviews_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CustomerId",
                table: "RestaurantReviews",
                newName: "IX_RestaurantReviews_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantReviews",
                table: "RestaurantReviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantReviews_Customers_CustomerId",
                table: "RestaurantReviews",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                table: "RestaurantReviews",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantReviews_Customers_CustomerId",
                table: "RestaurantReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                table: "RestaurantReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantReviews",
                table: "RestaurantReviews");

            migrationBuilder.RenameTable(
                name: "RestaurantReviews",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantReviews_RestaurantId",
                table: "Reviews",
                newName: "IX_Reviews_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantReviews_CustomerId",
                table: "Reviews",
                newName: "IX_Reviews_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_CustomerId",
                table: "Reviews",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantId",
                table: "Reviews",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
