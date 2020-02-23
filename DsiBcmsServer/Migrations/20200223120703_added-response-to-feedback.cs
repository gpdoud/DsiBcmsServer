using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class addedresponsetofeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "Feedbacks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Response",
                table: "Feedbacks");
        }
    }
}
