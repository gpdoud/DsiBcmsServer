using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class correctedevalpointsscoredname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrintsScored",
                table: "Evaluations");

            migrationBuilder.AddColumn<int>(
                name: "PointsScored",
                table: "Evaluations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsScored",
                table: "Evaluations");

            migrationBuilder.AddColumn<int>(
                name: "PrintsScored",
                table: "Evaluations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
