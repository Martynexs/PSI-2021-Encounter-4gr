using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EncounterAPI.Migrations
{
    public partial class updatemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ouizzes_Waypoints_WaypointID",
                table: "Ouizzes");

            migrationBuilder.DropIndex(
                name: "IX_Ouizzes_WaypointID",
                table: "Ouizzes");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Ouizzes");

            migrationBuilder.RenameColumn(
                name: "WaypointID",
                table: "Ouizzes",
                newName: "WaypointId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastVisit",
                table: "RouteCompletions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Ouizzes_WaypointId",
                table: "Ouizzes",
                column: "WaypointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ouizzes_Waypoints_WaypointId",
                table: "Ouizzes",
                column: "WaypointId",
                principalTable: "Waypoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ouizzes_Waypoints_WaypointId",
                table: "Ouizzes");

            migrationBuilder.DropIndex(
                name: "IX_Ouizzes_WaypointId",
                table: "Ouizzes");

            migrationBuilder.DropColumn(
                name: "LastVisit",
                table: "RouteCompletions");

            migrationBuilder.RenameColumn(
                name: "WaypointId",
                table: "Ouizzes",
                newName: "WaypointID");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Ouizzes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ouizzes_WaypointID",
                table: "Ouizzes",
                column: "WaypointID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ouizzes_Waypoints_WaypointID",
                table: "Ouizzes",
                column: "WaypointID",
                principalTable: "Waypoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
