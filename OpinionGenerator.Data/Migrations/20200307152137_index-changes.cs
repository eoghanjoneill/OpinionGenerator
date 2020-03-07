using Microsoft.EntityFrameworkCore.Migrations;

namespace OpinionGenerator.Data.Migrations
{
    public partial class indexchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_NewsSource_SourceIntId",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsSource",
                table: "NewsSource");

            migrationBuilder.RenameTable(
                name: "NewsSource",
                newName: "ArticleNewsSource");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleNewsSource",
                table: "ArticleNewsSource",
                column: "IntId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleNewsSource_Id_Name",
                table: "ArticleNewsSource",
                columns: new[] { "Id", "Name" });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleNewsSource_SourceIntId",
                table: "Articles",
                column: "SourceIntId",
                principalTable: "ArticleNewsSource",
                principalColumn: "IntId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleNewsSource_SourceIntId",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleNewsSource",
                table: "ArticleNewsSource");

            migrationBuilder.DropIndex(
                name: "IX_ArticleNewsSource_Id_Name",
                table: "ArticleNewsSource");

            migrationBuilder.RenameTable(
                name: "ArticleNewsSource",
                newName: "NewsSource");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsSource",
                table: "NewsSource",
                column: "IntId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_NewsSource_SourceIntId",
                table: "Articles",
                column: "SourceIntId",
                principalTable: "NewsSource",
                principalColumn: "IntId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
