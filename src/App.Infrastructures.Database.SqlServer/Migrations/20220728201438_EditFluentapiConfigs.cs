using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructures.Database.SqlServer.Migrations
{
    public partial class EditFluentapiConfigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceComments_Orders",
                table: "ServiceComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceComments_Services",
                table: "ServiceComments");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceComments_Orders",
                table: "ServiceComments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceComments_Services",
                table: "ServiceComments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceComments_Orders",
                table: "ServiceComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceComments_Services",
                table: "ServiceComments");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceComments_Orders",
                table: "ServiceComments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceComments_Services",
                table: "ServiceComments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");
        }
    }
}
