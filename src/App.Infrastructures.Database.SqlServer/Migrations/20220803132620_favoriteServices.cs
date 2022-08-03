using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructures.Database.SqlServer.Migrations
{
    public partial class favoriteServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertFavoriteCategories_Categories",
                table: "ExpertFavoriteCategories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ExpertFavoriteCategories",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpertFavoriteCategories_CategoryId",
                table: "ExpertFavoriteCategories",
                newName: "IX_ExpertFavoriteCategories_ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertFavoriteCategories_Categories",
                table: "ExpertFavoriteCategories",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertFavoriteCategories_Categories",
                table: "ExpertFavoriteCategories");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "ExpertFavoriteCategories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpertFavoriteCategories_ServiceId",
                table: "ExpertFavoriteCategories",
                newName: "IX_ExpertFavoriteCategories_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertFavoriteCategories_Categories",
                table: "ExpertFavoriteCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
