using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSI.BcmsServer.Migrations
{
    public partial class cascadedeletecalendardays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarDays_Calendars_CalendarId",
                table: "CalendarDays");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarDays_Calendars_CalendarId",
                table: "CalendarDays",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalendarDays_Calendars_CalendarId",
                table: "CalendarDays");

            migrationBuilder.AddForeignKey(
                name: "FK_CalendarDays_Calendars_CalendarId",
                table: "CalendarDays",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
