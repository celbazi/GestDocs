using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestDoc.Migrations
{
    public partial class ContentImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Document",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Document");
        }
    }
}
