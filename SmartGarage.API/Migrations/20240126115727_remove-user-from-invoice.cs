using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartGarage.API.Migrations
{
    public partial class removeuserfrominvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_AspNetUsers_ApplicationUserId1",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ApplicationUserId1",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Invoice");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Invoice",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ApplicationUserId",
                table: "Invoice",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_AspNetUsers_ApplicationUserId",
                table: "Invoice",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_AspNetUsers_ApplicationUserId",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_ApplicationUserId",
                table: "Invoice");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Invoice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Invoice",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ApplicationUserId1",
                table: "Invoice",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_AspNetUsers_ApplicationUserId1",
                table: "Invoice",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
