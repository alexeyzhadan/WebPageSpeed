using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPageSpeed.Data.Migrations
{
    public partial class AddWebSiteAndWebPageTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "WebPages");

            migrationBuilder.DropColumn(
                name: "Uri",
                table: "WebPages");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "WebPages",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "WebSiteId",
                table: "WebPages",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "WebSites",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Authority = table.Column<string>(nullable: false),
                    DateOfAnalysis = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebPages_WebSiteId",
                table: "WebPages",
                column: "WebSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_WebPages_WebSites_WebSiteId",
                table: "WebPages",
                column: "WebSiteId",
                principalTable: "WebSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebPages_WebSites_WebSiteId",
                table: "WebPages");

            migrationBuilder.DropTable(
                name: "WebSites");

            migrationBuilder.DropIndex(
                name: "IX_WebPages_WebSiteId",
                table: "WebPages");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "WebPages");

            migrationBuilder.DropColumn(
                name: "WebSiteId",
                table: "WebPages");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "WebPages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Uri",
                table: "WebPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
