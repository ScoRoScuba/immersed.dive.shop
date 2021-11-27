using Microsoft.EntityFrameworkCore.Migrations;

namespace immersed.dive.shop.repository.Migrations
{
    public partial class addeventdatestocontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventDate_Events_EventId",
                table: "EventDate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventDate",
                table: "EventDate");

            migrationBuilder.RenameTable(
                name: "EventDate",
                newName: "EventDates");

            migrationBuilder.RenameIndex(
                name: "IX_EventDate_EventId",
                table: "EventDates",
                newName: "IX_EventDates_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventDates",
                table: "EventDates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventDates_Events_EventId",
                table: "EventDates",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventDates_Events_EventId",
                table: "EventDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventDates",
                table: "EventDates");

            migrationBuilder.RenameTable(
                name: "EventDates",
                newName: "EventDate");

            migrationBuilder.RenameIndex(
                name: "IX_EventDates_EventId",
                table: "EventDate",
                newName: "IX_EventDate_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventDate",
                table: "EventDate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventDate_Events_EventId",
                table: "EventDate",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
