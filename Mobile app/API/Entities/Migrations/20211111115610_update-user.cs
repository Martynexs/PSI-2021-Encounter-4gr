using Microsoft.EntityFrameworkCore.Migrations;

namespace EncounterAPI.Migrations
{
    public partial class updateuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Routes",
                newName: "CreatorID");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_CreatorID",
                table: "Routes",
                column: "CreatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Users_CreatorID",
                table: "Routes",
                column: "CreatorID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Users_CreatorID",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Routes_CreatorID",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CreatorID",
                table: "Routes",
                newName: "CreatorId");

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }
    }
}
