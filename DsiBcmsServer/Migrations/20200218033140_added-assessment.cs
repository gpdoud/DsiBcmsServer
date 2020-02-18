using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class addedassessment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PointsScore = table.Column<int>(nullable: true),
                    PointsMax = table.Column<int>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    EnrollmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessments_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_EnrollmentId",
                table: "Assessments",
                column: "EnrollmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assessments");
        }
    }
}
