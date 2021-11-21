using Microsoft.EntityFrameworkCore.Migrations;

namespace EncounterAPI.Migrations
{
    public partial class CreateQuizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Quizes",
            columns: table => new
            {
                Id = table.Column<long>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                WaypointId = table.Column<int>(type: "INTEGER", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Quiz", x => x.Id);
                table.ForeignKey(
                      name: "FK_Quiz_Waypoint_Id",
                      column: x => x.WaypointId,
                      principalTable: "Waypoints",
                      principalColumn: "Id");
            });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quizes");
        }
    }
}
