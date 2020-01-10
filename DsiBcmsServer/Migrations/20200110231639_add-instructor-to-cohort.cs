using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class addinstructortocohort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstructorId",
                table: "Cohorts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_InstructorId",
                table: "Cohorts",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cohorts_Users_InstructorId",
                table: "Cohorts",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cohorts_Users_InstructorId",
                table: "Cohorts");

            migrationBuilder.DropIndex(
                name: "IX_Cohorts_InstructorId",
                table: "Cohorts");

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "Cohorts");
        }
    }
}
