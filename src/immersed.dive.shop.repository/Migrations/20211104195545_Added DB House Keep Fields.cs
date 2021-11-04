using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace immersed.dive.shop.repository.Migrations
{
    public partial class AddedDBHouseKeepFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Persons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Persons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Live",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Live",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateConfirmed",
                table: "CourseParticipants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "CourseParticipants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegistered",
                table: "CourseParticipants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CourseParticipants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "CourseParticipants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Live",
                table: "CourseParticipants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Live",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Live",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DateConfirmed",
                table: "CourseParticipants");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "CourseParticipants");

            migrationBuilder.DropColumn(
                name: "DateRegistered",
                table: "CourseParticipants");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseParticipants");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "CourseParticipants");

            migrationBuilder.DropColumn(
                name: "Live",
                table: "CourseParticipants");
        }
    }
}
