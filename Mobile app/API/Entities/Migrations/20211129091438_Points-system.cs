using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EncounterAPI.Migrations
{
    public partial class Pointssystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Waypoints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ouizzes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WaypointId = table.Column<long>(type: "INTEGER", nullable: false),
                    Question = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ouizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ouizzes_Waypoints_WaypointId",
                        column: x => x.WaypointId,
                        principalTable: "Waypoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteCompletions",
                columns: table => new
                {
                    RouteId = table.Column<long>(type: "INTEGER", nullable: false),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    LastVisit = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteCompletions", x => new { x.RouteId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RouteCompletions_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteCompletions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizzAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuizId = table.Column<long>(type: "INTEGER", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: true),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizzAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizzAnswers_Ouizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Ouizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaypointCompletions",
                columns: table => new
                {
                    WaypointId = table.Column<long>(type: "INTEGER", nullable: false),
                    RouteCompletionUserId = table.Column<long>(type: "INTEGER", nullable: false),
                    RouteCompletionRouteId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaypointCompletions", x => new { x.RouteCompletionRouteId, x.RouteCompletionUserId, x.WaypointId });
                    table.ForeignKey(
                        name: "FK_WaypointCompletions_RouteCompletions_RouteCompletionRouteId_RouteCompletionUserId",
                        columns: x => new { x.RouteCompletionRouteId, x.RouteCompletionUserId },
                        principalTable: "RouteCompletions",
                        principalColumns: new[] { "RouteId", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaypointCompletions_Waypoints_WaypointId",
                        column: x => x.WaypointId,
                        principalTable: "Waypoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ouizzes_WaypointId",
                table: "Ouizzes",
                column: "WaypointId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizzAnswers_QuizId",
                table: "QuizzAnswers",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteCompletions_UserId",
                table: "RouteCompletions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WaypointCompletions_WaypointId",
                table: "WaypointCompletions",
                column: "WaypointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizzAnswers");

            migrationBuilder.DropTable(
                name: "WaypointCompletions");

            migrationBuilder.DropTable(
                name: "Ouizzes");

            migrationBuilder.DropTable(
                name: "RouteCompletions");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Waypoints");
        }
    }
}
