using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EncounterAPI.Migrations
{
    public partial class AddType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ClosingTime",
                table: "Waypoints",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OpeningHours",
                table: "Waypoints",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Waypoints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Waypoints",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Waypoints",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingTime",
                table: "Waypoints");

            migrationBuilder.DropColumn(
                name: "OpeningHours",
                table: "Waypoints");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Waypoints");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Waypoints");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Waypoints");
        }
    }
}
