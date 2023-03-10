using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerApp.Migrations;

public partial class Tags : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ArticleTag");

        migrationBuilder.CreateTable(
            name: "ArticleTags",
            columns: table => new
            {
                ArticleId = table.Column<long>(type: "INTEGER", nullable: false),
                TagName = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ArticleTags", x => new { x.ArticleId, x.TagName });
                table.ForeignKey(
                    name: "FK_ArticleTags_Articles_ArticleId",
                    column: x => x.ArticleId,
                    principalTable: "Articles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ArticleTags_Tags_TagName",
                    column: x => x.TagName,
                    principalTable: "Tags",
                    principalColumn: "Name",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ArticleTags_TagName",
            table: "ArticleTags",
            column: "TagName");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ArticleTags");

        migrationBuilder.CreateTable(
            name: "ArticleTag",
            columns: table => new
            {
                ArticlesId = table.Column<long>(type: "INTEGER", nullable: false),
                TagsName = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ArticleTag", x => new { x.ArticlesId, x.TagsName });
                table.ForeignKey(
                    name: "FK_ArticleTag_Articles_ArticlesId",
                    column: x => x.ArticlesId,
                    principalTable: "Articles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_ArticleTag_Tags_TagsName",
                    column: x => x.TagsName,
                    principalTable: "Tags",
                    principalColumn: "Name",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ArticleTag_TagsName",
            table: "ArticleTag",
            column: "TagsName");
    }
}
