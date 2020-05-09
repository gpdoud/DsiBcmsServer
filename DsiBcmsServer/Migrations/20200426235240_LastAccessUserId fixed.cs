using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class LastAccessUserIdfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Users_StudentId",
                table: "Commentaries");

            migrationBuilder.AddColumn<bool>(
                name: "Sensitive",
                table: "Commentaries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_LastAcessUserId",
                table: "Commentaries",
                column: "LastAcessUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Users_LastAcessUserId",
                table: "Commentaries",
                column: "LastAcessUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Users_StudentId",
                table: "Commentaries",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Users_LastAcessUserId",
                table: "Commentaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Users_StudentId",
                table: "Commentaries");

            migrationBuilder.DropIndex(
                name: "IX_Commentaries_LastAcessUserId",
                table: "Commentaries");

            migrationBuilder.DropColumn(
                name: "Sensitive",
                table: "Commentaries");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Users_StudentId",
                table: "Commentaries",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
