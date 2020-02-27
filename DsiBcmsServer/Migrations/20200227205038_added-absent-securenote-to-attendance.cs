using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class addedabsentsecurenotetoattendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Absent",
                table: "Attendance",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecureNote",
                table: "Attendance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Absent",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "SecureNote",
                table: "Attendance");
        }
    }
}
