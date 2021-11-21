using Microsoft.EntityFrameworkCore.Migrations;

namespace EncounterAPI.Migrations
{
    public partial class CreateUserAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionId = table.Column<long>(type: "INTEGER", nullable: false),
                    ActualChoiceId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswer", x => x.Id);

                    table.ForeignKey(
                          name: "FK_UserAnswers_UserId",
                          column: x => x.UserId,
                          principalTable: "Users",
                          principalColumn: "ID");

                    table.ForeignKey(
                          name: "FK_UserAnswers_QuestionId",
                          column: x => x.QuestionId,
                          principalTable: "Questions",
                          principalColumn: "Id");

                    table.ForeignKey(
                          name: "FK_UserAnswers_ActualChoiceId",
                          column: x => x.ActualChoiceId,
                          principalTable: "QuestionChoices",
                          principalColumn: "Id");
                });

        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnswers");
        }
    }
}
