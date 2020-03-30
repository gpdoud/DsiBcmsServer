using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class v80initaddedevaluationquestionclasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 80, nullable: false),
                    IsTemplate = table.Column<bool>(nullable: false),
                    EnrollmentId = table.Column<int>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(maxLength: 30, nullable: false),
                    QuestionText = table.Column<string>(maxLength: 255, nullable: false),
                    AnswerTextA = table.Column<string>(maxLength: 80, nullable: false),
                    AnswerTextB = table.Column<string>(maxLength: 80, nullable: false),
                    AnswerTextC = table.Column<string>(maxLength: 80, nullable: true),
                    AnswerTextD = table.Column<string>(maxLength: 80, nullable: true),
                    AnswerTextE = table.Column<string>(maxLength: 80, nullable: true),
                    CorrectAnswerNbr = table.Column<int>(nullable: false),
                    PointValue = table.Column<int>(nullable: false),
                    UserAnswerNbr = table.Column<int>(nullable: false),
                    EvaluationId = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Evaluations_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EnrollmentId",
                table: "Evaluations",
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
