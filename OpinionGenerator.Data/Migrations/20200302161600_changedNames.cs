using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpinionGenerator.Data.Migrations
{
    public partial class changedNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AnalyticsResults_TextAnalyticsResultId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_SentenceSentiment_AnalyticsResults_TextAnalyticsResultId",
                table: "SentenceSentiment");

            migrationBuilder.DropTable(
                name: "AnalyticsResults");

            migrationBuilder.DropIndex(
                name: "IX_SentenceSentiment_TextAnalyticsResultId",
                table: "SentenceSentiment");

            migrationBuilder.DropIndex(
                name: "IX_NewsSource_Id_Name",
                table: "NewsSource");

            migrationBuilder.DropColumn(
                name: "TextAnalyticsResultId",
                table: "SentenceSentiment");

            migrationBuilder.AddColumn<int>(
                name: "AzTextAnalyticsResultId",
                table: "SentenceSentiment",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NewsSource",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "NewsSource",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UrlToImage",
                table: "Articles",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Articles",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "RetrievedAt",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PublishedAt",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Articles",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Articles",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Articles",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AzTextAnalyticsResults",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextAnalyzed = table.Column<string>(nullable: true),
                    Sentiment = table.Column<int>(nullable: false),
                    SentimentScoresId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AzTextAnalyticsResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AzTextAnalyticsResults_SentimentScorePerLabel_SentimentScoresId",
                        column: x => x.SentimentScoresId,
                        principalTable: "SentimentScorePerLabel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SentenceSentiment_AzTextAnalyticsResultId",
                table: "SentenceSentiment",
                column: "AzTextAnalyticsResultId");

            migrationBuilder.CreateIndex(
                name: "IX_AzTextAnalyticsResults_SentimentScoresId",
                table: "AzTextAnalyticsResults",
                column: "SentimentScoresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AzTextAnalyticsResults_TextAnalyticsResultId",
                table: "Articles",
                column: "TextAnalyticsResultId",
                principalTable: "AzTextAnalyticsResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SentenceSentiment_AzTextAnalyticsResults_AzTextAnalyticsResultId",
                table: "SentenceSentiment",
                column: "AzTextAnalyticsResultId",
                principalTable: "AzTextAnalyticsResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AzTextAnalyticsResults_TextAnalyticsResultId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_SentenceSentiment_AzTextAnalyticsResults_AzTextAnalyticsResultId",
                table: "SentenceSentiment");

            migrationBuilder.DropTable(
                name: "AzTextAnalyticsResults");

            migrationBuilder.DropIndex(
                name: "IX_SentenceSentiment_AzTextAnalyticsResultId",
                table: "SentenceSentiment");

            migrationBuilder.DropColumn(
                name: "AzTextAnalyticsResultId",
                table: "SentenceSentiment");

            migrationBuilder.AddColumn<int>(
                name: "TextAnalyticsResultId",
                table: "SentenceSentiment",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NewsSource",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "NewsSource",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UrlToImage",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RetrievedAt",
                table: "Articles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishedAt",
                table: "Articles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AnalyticsResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sentiment = table.Column<int>(type: "int", nullable: false),
                    SentimentScoresId = table.Column<int>(type: "int", nullable: true),
                    TextAnalyzed = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticsResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalyticsResults_SentimentScorePerLabel_SentimentScoresId",
                        column: x => x.SentimentScoresId,
                        principalTable: "SentimentScorePerLabel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SentenceSentiment_TextAnalyticsResultId",
                table: "SentenceSentiment",
                column: "TextAnalyticsResultId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsSource_Id_Name",
                table: "NewsSource",
                columns: new[] { "Id", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticsResults_SentimentScoresId",
                table: "AnalyticsResults",
                column: "SentimentScoresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AnalyticsResults_TextAnalyticsResultId",
                table: "Articles",
                column: "TextAnalyticsResultId",
                principalTable: "AnalyticsResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SentenceSentiment_AnalyticsResults_TextAnalyticsResultId",
                table: "SentenceSentiment",
                column: "TextAnalyticsResultId",
                principalTable: "AnalyticsResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
