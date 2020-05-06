using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class changesnamelastAccessUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Users_LastAcessUserId",
                table: "Commentaries");

            migrationBuilder.DropIndex(
                name: "IX_Commentaries_LastAcessUserId",
                table: "Commentaries");

            migrationBuilder.DropColumn(
                name: "LastAcessUserId",
                table: "Commentaries");

            migrationBuilder.AddColumn<int>(
                name: "LastAccessUserId",
                table: "Commentaries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_LastAccessUserId",
                table: "Commentaries",
                column: "LastAccessUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Users_LastAccessUserId",
                table: "Commentaries",
                column: "LastAccessUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Users_LastAccessUserId",
                table: "Commentaries");

            migrationBuilder.DropIndex(
                name: "IX_Commentaries_LastAccessUserId",
                table: "Commentaries");

            migrationBuilder.DropColumn(
                name: "LastAccessUserId",
                table: "Commentaries");

            migrationBuilder.AddColumn<int>(
                name: "LastAcessUserId",
                table: "Commentaries",
                type: "int",
                nullable: true);

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
        }
    }
}
