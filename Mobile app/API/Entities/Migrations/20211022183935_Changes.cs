using Microsoft.EntityFrameworkCore.Migrations;

namespace EncounterAPI.Migrations
{
    public partial class Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Waypoints_RouteId",
                table: "Waypoints",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Waypoints_Routes_RouteId",
                table: "Waypoints",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Waypoints_Routes_RouteId",
                table: "Waypoints");

            migrationBuilder.DropIndex(
                name: "IX_Waypoints_RouteId",
                table: "Waypoints");
        }
    }
}
