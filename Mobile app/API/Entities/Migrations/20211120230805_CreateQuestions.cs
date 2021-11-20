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
                Question_Text = table.Column<string>(type: "TEXT", nullable: false),
                Question_Type = table.Column<string>(type: "TEXT", nullable: false),
                Question_Score = table.Column<long>(type: "INTEGER", nullable: true),
                Question_Timer = table.Column<long>(type: "INTEGER", nullable: true),
                Quiz_Id = table.Column<long>(type: "INTEGER", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Question", x => x.Id);

                table.ForeignKey(
               name: "FK_Question_Quiz_Id",
               column: x => x.Quiz_Id,
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
