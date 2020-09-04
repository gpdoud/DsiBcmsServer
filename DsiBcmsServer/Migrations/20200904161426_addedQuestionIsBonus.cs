using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class addedQuestionIsBonus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBonus",
                table: "Questions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBonus",
                table: "Questions");
        }
    }
}
