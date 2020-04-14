using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class addedkbkbCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KbCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KbCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kbs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Text = table.Column<string>(nullable: false),
                    Response = table.Column<string>(nullable: true),
                    Sticky = table.Column<bool>(nullable: false),
                    Locked = table.Column<bool>(nullable: false),
                    NextId = table.Column<int>(nullable: false),
                    PrevId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    KbCategoryId = table.Column<int>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kbs_KbCategories_KbCategoryId",
                        column: x => x.KbCategoryId,
                        principalTable: "KbCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kbs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kbs_KbCategoryId",
                table: "Kbs",
                column: "KbCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Kbs_UserId",
                table: "Kbs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kbs");

            migrationBuilder.DropTable(
                name: "KbCategories");
        }
    }
}
