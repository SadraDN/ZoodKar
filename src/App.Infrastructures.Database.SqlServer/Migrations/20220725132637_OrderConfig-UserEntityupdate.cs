using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructures.Database.SqlServer.Migrations
{
    public partial class OrderConfigUserEntityupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_FinalExpertUserId",
                table: "Orders",
                column: "FinalExpertUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_FinalExpertUserId",
                table: "Orders",
                column: "FinalExpertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_FinalExpertUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_FinalExpertUserId",
                table: "Orders");
        }
    }
}
