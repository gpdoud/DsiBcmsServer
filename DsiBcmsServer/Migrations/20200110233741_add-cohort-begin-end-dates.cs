using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class addcohortbeginenddates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Start",
                table: "Cohorts");

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginDate",
                table: "Cohorts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Cohorts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginDate",
                table: "Cohorts");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Cohorts");

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Cohorts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
