using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DSI.BcmsServer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysCtrls",
                columns: table => new
                {
                    Key = table.Column<string>(maxLength: 50, nullable: false),
                    Value = table.Column<string>(maxLength: 80, nullable: true),
                    Category = table.Column<string>(maxLength: 30, nullable: true),
                    Active = table.Column<bool>(nullable: false, defaultValue: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysCtrls", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SysCtrls_Key",
                table: "SysCtrls",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysCtrls");
        }
    }
}
