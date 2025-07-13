using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Restaurant_Reservation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePasswordToHashedPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyTypes_Hotels_HotelId",
                table: "CurrencyTypes");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyTypes_HotelId",
                table: "CurrencyTypes");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "CurrencyTypes");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Customers",
                newName: "HashedPassword");

            migrationBuilder.CreateTable(
                name: "HotelCurrencyType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HotelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelCurrencyType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelCurrencyType_CurrencyTypes_CurrencyTypeId",
                        column: x => x.CurrencyTypeId,
                        principalTable: "CurrencyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelCurrencyType_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelCurrencyType_CurrencyTypeId",
                table: "HotelCurrencyType",
                column: "CurrencyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelCurrencyType_HotelId",
                table: "HotelCurrencyType",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelCurrencyType");

            migrationBuilder.RenameColumn(
                name: "HashedPassword",
                table: "Customers",
                newName: "Password");

            migrationBuilder.AddColumn<Guid>(
                name: "HotelId",
                table: "CurrencyTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyTypes_HotelId",
                table: "CurrencyTypes",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyTypes_Hotels_HotelId",
                table: "CurrencyTypes",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");
        }
    }
}
