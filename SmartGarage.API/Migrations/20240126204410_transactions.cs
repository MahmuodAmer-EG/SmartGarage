using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGarage.API.Migrations
{
    public partial class transactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarPlaceId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPlaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTransactions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CarPlaceId",
                table: "AspNetUsers",
                column: "CarPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CarPlaces_CarPlaceId",
                table: "AspNetUsers",
                column: "CarPlaceId",
                principalTable: "CarPlaces",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CarPlaces_CarPlaceId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CarPlaces");

            migrationBuilder.DropTable(
                name: "UnitTransactions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CarPlaceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarPlaceId",
                table: "AspNetUsers");
        }
    }
}
