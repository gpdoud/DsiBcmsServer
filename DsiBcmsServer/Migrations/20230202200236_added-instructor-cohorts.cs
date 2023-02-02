using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSI.BcmsServer.Migrations
{
    public partial class addedinstructorcohorts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cohorts_Users_InstructorId",
                table: "Cohorts");

            migrationBuilder.DropIndex(
                name: "IX_Cohorts_InstructorId",
                table: "Cohorts");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cohorts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InstructorCohorts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    CohortId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorCohorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructorCohorts_Cohorts_CohortId",
                        column: x => x.CohortId,
                        principalTable: "Cohorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorCohorts_Users_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_UserId",
                table: "Cohorts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorCohorts_CohortId",
                table: "InstructorCohorts",
                column: "CohortId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorCohorts_InstructorId",
                table: "InstructorCohorts",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cohorts_Users_UserId",
                table: "Cohorts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cohorts_Users_UserId",
                table: "Cohorts");

            migrationBuilder.DropTable(
                name: "InstructorCohorts");

            migrationBuilder.DropIndex(
                name: "IX_Cohorts_UserId",
                table: "Cohorts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cohorts");

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
    }
}
