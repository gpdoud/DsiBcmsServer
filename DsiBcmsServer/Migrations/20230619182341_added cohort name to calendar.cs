using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSI.BcmsServer.Migrations
{
    public partial class addedcohortnametocalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cohorts_CalendarId",
                table: "Cohorts");

            migrationBuilder.AddColumn<string>(
                name: "CohortName",
                table: "Calendars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_CalendarId",
                table: "Cohorts",
                column: "CalendarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cohorts_CalendarId",
                table: "Cohorts");

            migrationBuilder.DropColumn(
                name: "CohortName",
                table: "Calendars");

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_CalendarId",
                table: "Cohorts",
                column: "CalendarId",
                unique: true,
                filter: "[CalendarId] IS NOT NULL");
        }
    }
}
