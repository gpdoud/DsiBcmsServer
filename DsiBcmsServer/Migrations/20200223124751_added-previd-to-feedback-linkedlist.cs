using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class addedprevidtofeedbacklinkedlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrevId",
                table: "Feedbacks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrevId",
                table: "Feedbacks");
        }
    }
}
