using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSI.BcmsServer.Migrations
{
    public partial class addedtemplatetocalendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Template",
                table: "Calendars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Template",
                table: "Calendars");
        }
    }
}
