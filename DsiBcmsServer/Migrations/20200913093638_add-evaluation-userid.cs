using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class addevaluationuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Evaluations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_UserId",
                table: "Evaluations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Users_UserId",
                table: "Evaluations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Users_UserId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_UserId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Evaluations");
        }
    }
}
