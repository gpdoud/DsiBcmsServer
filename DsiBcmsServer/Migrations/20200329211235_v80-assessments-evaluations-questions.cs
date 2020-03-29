using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class v80assessmentsevaluationsquestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(nullable: true),
                    QuestionText = table.Column<string>(nullable: true),
                    AnswerTextA = table.Column<string>(nullable: true),
                    AnswerTextB = table.Column<string>(nullable: true),
                    AnswerTextC = table.Column<string>(nullable: true),
                    AnswerTextD = table.Column<string>(nullable: true),
                    AnswerTextE = table.Column<string>(nullable: true),
                    CorrectAnswerNbr = table.Column<int>(nullable: false),
                    PointValue = table.Column<int>(nullable: false),
                    UserAnswerNbr = table.Column<int>(nullable: false),
                    EvaluationId = table.Column<int>(nullable: false),
                    EnrollmentId = table.Column<int>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Evaluations_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_EnrollmentId",
                table: "Questions",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_EvaluationId",
                table: "Questions",
                column: "EvaluationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Evaluations");
        }
    }
}
