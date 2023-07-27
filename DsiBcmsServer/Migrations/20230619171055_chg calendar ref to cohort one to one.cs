using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSI.BcmsServer.Migrations
{
    public partial class chgcalendarreftocohortonetoone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cohorts_CalendarId",
                table: "Cohorts");

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_CalendarId",
                table: "Cohorts",
                column: "CalendarId",
                unique: true,
                filter: "[CalendarId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cohorts_CalendarId",
                table: "Cohorts");

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_CalendarId",
                table: "Cohorts",
                column: "CalendarId");
        }
    }
}
