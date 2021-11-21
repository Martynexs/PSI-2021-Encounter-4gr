using Microsoft.EntityFrameworkCore.Migrations;

namespace EncounterAPI.Migrations
{
    public partial class CreateQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "Questions",
            columns: table => new
            {
                Id = table.Column<long>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Position = table.Column<long>(type: "INTEGER", nullable: true),
                Text = table.Column<string>(type: "TEXT", nullable: false),
                Type = table.Column<string>(type: "TEXT", nullable: false),
                SecondsToAnswer = table.Column<long>(type: "INTEGER", nullable: true),
                QuizId = table.Column<long>(type: "INTEGER", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Question", x => x.Id);

                table.ForeignKey(
                   name: "FK_Question_Quiz_Id",
                   column: x => x.QuizId,
                   principalTable: "Quizes",
                   principalColumn: "Id");
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
