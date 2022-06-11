using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabetesTracker.Migrations
{
    public partial class AddImagecolumntoBlogstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Blogs",
                type: "image",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Blogs");
        }
    }
}
