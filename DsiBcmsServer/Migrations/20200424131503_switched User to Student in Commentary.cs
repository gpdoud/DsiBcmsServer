using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class switchedUsertoStudentinCommentary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Users_UserId",
                table: "Commentaries");

            migrationBuilder.DropIndex(
                name: "IX_Commentaries_UserId",
                table: "Commentaries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Commentaries");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Commentaries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_StudentId",
                table: "Commentaries",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Users_StudentId",
                table: "Commentaries",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Users_StudentId",
                table: "Commentaries");

            migrationBuilder.DropIndex(
                name: "IX_Commentaries_StudentId",
                table: "Commentaries");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Commentaries");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Commentaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_UserId",
                table: "Commentaries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Users_UserId",
                table: "Commentaries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
