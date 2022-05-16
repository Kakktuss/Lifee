using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lifee.RSS.Application.DataAccess.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RssFeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RssFeedConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    FeedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeedConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RssFeedConfigurations_RssFeeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RssFeedItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RssFeedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RssFeedItems_RssFeeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryRssFeed",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    RssFeedsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRssFeed", x => new { x.CategoriesId, x.RssFeedsId });
                    table.ForeignKey(
                        name: "FK_CategoryRssFeed_RssFeeds_RssFeedsId",
                        column: x => x.RssFeedsId,
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryRssFeed_Tags_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRssFeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RssFeedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRssFeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRssFeeds_RssFeeds_RssFeedId",
                        column: x => x.RssFeedId,
                        principalTable: "RssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRssFeeds_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRssFeedTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRssFeedTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRssFeedTags_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRssFeedUserRssFeedTag",
                columns: table => new
                {
                    RssFeedsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRssFeedUserRssFeedTag", x => new { x.RssFeedsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_UserRssFeedUserRssFeedTag_UserRssFeeds_RssFeedsId",
                        column: x => x.RssFeedsId,
                        principalTable: "UserRssFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRssFeedUserRssFeedTag_UserRssFeedTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "UserRssFeedTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRssFeed_RssFeedsId",
                table: "CategoryRssFeed",
                column: "RssFeedsId");

            migrationBuilder.CreateIndex(
                name: "IX_RssFeedConfigurations_FeedId",
                table: "RssFeedConfigurations",
                column: "FeedId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RssFeedItems_FeedId",
                table: "RssFeedItems",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRssFeeds_RssFeedId",
                table: "UserRssFeeds",
                column: "RssFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRssFeeds_UserId",
                table: "UserRssFeeds",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRssFeedTags_UserId",
                table: "UserRssFeedTags",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRssFeedUserRssFeedTag_TagsId",
                table: "UserRssFeedUserRssFeedTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryRssFeed");

            migrationBuilder.DropTable(
                name: "RssFeedConfigurations");

            migrationBuilder.DropTable(
                name: "RssFeedItems");

            migrationBuilder.DropTable(
                name: "UserRssFeedUserRssFeedTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "UserRssFeeds");

            migrationBuilder.DropTable(
                name: "UserRssFeedTags");

            migrationBuilder.DropTable(
                name: "RssFeeds");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
