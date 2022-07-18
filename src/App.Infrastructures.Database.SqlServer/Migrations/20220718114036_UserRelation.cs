using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructures.Database.SqlServer.Migrations
{
    public partial class UserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerUserId",
                table: "Orders",
                column: "CustomerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CreatedUserId",
                table: "Files",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertFavoriteCategories_ExpertUserId",
                table: "ExpertFavoriteCategories",
                column: "ExpertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_ExpertUserId",
                table: "Bids",
                column: "ExpertUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_AspNetUsers_ExpertUserId",
                table: "Bids",
                column: "ExpertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertFavoriteCategories_AspNetUsers_ExpertUserId",
                table: "ExpertFavoriteCategories",
                column: "ExpertUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_AspNetUsers_CreatedUserId",
                table: "Files",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerUserId",
                table: "Orders",
                column: "CustomerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_AspNetUsers_ExpertUserId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpertFavoriteCategories_AspNetUsers_ExpertUserId",
                table: "ExpertFavoriteCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_AspNetUsers_CreatedUserId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Files_CreatedUserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_ExpertFavoriteCategories_ExpertUserId",
                table: "ExpertFavoriteCategories");

            migrationBuilder.DropIndex(
                name: "IX_Bids_ExpertUserId",
                table: "Bids");
        }
    }
}
