using Microsoft.EntityFrameworkCore.Migrations;

namespace EncounterAPI.Migrations
{
    public partial class CreateQuestionChoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "QuestionChoices",
            columns: table => new
            {
                Id = table.Column<long>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Position = table.Column<long>(type: "INTEGER", nullable: true),
                Letter = table.Column<string>(type: "CHAR", nullable: false),
                Text = table.Column<string>(type: "TEXT", nullable: false),
                IsRight = table.Column<int>(type: "INTEGER", nullable: false),
                SelectedScore = table.Column<double>(type: "REAL", nullable: true),
                UnselectedScore = table.Column<double>(type: "REAL", nullable: true),
                QuestionId = table.Column<long>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_QuestionChoice", x => x.Id);

                table.ForeignKey(
                  name: "FK_QuestionChoice_Answer_Id",
                  column: x => x.QuestionId,
                  principalTable: "Questions",
                  principalColumn: "Id");
            });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionChoices");
        }
    }
}
