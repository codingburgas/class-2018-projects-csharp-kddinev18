using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabetesTracker.Migrations
{
    public partial class RenametableFavouritestoFavouritePosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowingBlogs.BlogId",
                table: "FollowingBlogs");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowingBlogs.UserId",
                table: "FollowingBlogs");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_FollowingBlogs_FollowedOn",
                table: "FollowingBlogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FollowedOn",
                table: "FollowingBlogs",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "FavouritePosts",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    SavedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouritePosts", x => new { x.UserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_Favourites.PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favourites.UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouritePosts_PostId",
                table: "FavouritePosts",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowingBlogs.BlogId",
                table: "FollowingBlogs",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowingBlogs.UserId",
                table: "FollowingBlogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowingBlogs.BlogId",
                table: "FollowingBlogs");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowingBlogs.UserId",
                table: "FollowingBlogs");

            migrationBuilder.DropTable(
                name: "FavouritePosts");

            migrationBuilder.AlterColumn<int>(
                name: "FollowedOn",
                table: "FollowingBlogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    SavedOn = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => new { x.UserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_Favourites.PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Favourites.UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowingBlogs_FollowedOn",
                table: "FollowingBlogs",
                column: "FollowedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_PostId",
                table: "Favourites",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowingBlogs.BlogId",
                table: "FollowingBlogs",
                column: "UserId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowingBlogs.UserId",
                table: "FollowingBlogs",
                column: "FollowedOn",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
