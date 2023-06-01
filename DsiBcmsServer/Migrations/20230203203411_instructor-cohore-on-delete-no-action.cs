using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSI.BcmsServer.Migrations
{
    public partial class instructorcohoreondeletenoaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructorCohorts_Cohorts_CohortId",
                table: "InstructorCohorts");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorCohorts_Users_InstructorId",
                table: "InstructorCohorts");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorCohorts_Cohorts_CohortId",
                table: "InstructorCohorts",
                column: "CohortId",
                principalTable: "Cohorts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorCohorts_Users_InstructorId",
                table: "InstructorCohorts",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstructorCohorts_Cohorts_CohortId",
                table: "InstructorCohorts");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorCohorts_Users_InstructorId",
                table: "InstructorCohorts");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorCohorts_Cohorts_CohortId",
                table: "InstructorCohorts",
                column: "CohortId",
                principalTable: "Cohorts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorCohorts_Users_InstructorId",
                table: "InstructorCohorts",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
