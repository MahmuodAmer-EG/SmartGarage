using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGarage.API.Migrations
{
    public partial class deleteheightfromuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
