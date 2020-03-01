using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpinionGenerator.Data.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsSource",
                columns: table => new
                {
                    IntId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsSource", x => x.IntId);
                });

            migrationBuilder.CreateTable(
                name: "SentimentScorePerLabel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Positive = table.Column<double>(nullable: false),
                    Neutral = table.Column<double>(nullable: false),
                    Negative = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentimentScorePerLabel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticsResults",
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
                    table.PrimaryKey("PK_AnalyticsResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalyticsResults_SentimentScorePerLabel_SentimentScoresId",
                        column: x => x.SentimentScoresId,
                        principalTable: "SentimentScorePerLabel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RetrievedAt = table.Column<DateTime>(nullable: true),
                    TextAnalyticsResultId = table.Column<int>(nullable: true),
                    SourceIntId = table.Column<int>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    UrlToImage = table.Column<string>(nullable: true),
                    PublishedAt = table.Column<DateTime>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_NewsSource_SourceIntId",
                        column: x => x.SourceIntId,
                        principalTable: "NewsSource",
                        principalColumn: "IntId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Articles_AnalyticsResults_TextAnalyticsResultId",
                        column: x => x.TextAnalyticsResultId,
                        principalTable: "AnalyticsResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SentenceSentiment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sentiment = table.Column<int>(nullable: false),
                    SentimentScoresId = table.Column<int>(nullable: true),
                    Offset = table.Column<int>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    TextAnalyticsResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentenceSentiment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SentenceSentiment_SentimentScorePerLabel_SentimentScoresId",
                        column: x => x.SentimentScoresId,
                        principalTable: "SentimentScorePerLabel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SentenceSentiment_AnalyticsResults_TextAnalyticsResultId",
                        column: x => x.TextAnalyticsResultId,
                        principalTable: "AnalyticsResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticsResults_SentimentScoresId",
                table: "AnalyticsResults",
                column: "SentimentScoresId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_SourceIntId",
                table: "Articles",
                column: "SourceIntId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TextAnalyticsResultId",
                table: "Articles",
                column: "TextAnalyticsResultId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsSource_Id_Name",
                table: "NewsSource",
                columns: new[] { "Id", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_SentenceSentiment_SentimentScoresId",
                table: "SentenceSentiment",
                column: "SentimentScoresId");

            migrationBuilder.CreateIndex(
                name: "IX_SentenceSentiment_TextAnalyticsResultId",
                table: "SentenceSentiment",
                column: "TextAnalyticsResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "SentenceSentiment");

            migrationBuilder.DropTable(
                name: "NewsSource");

            migrationBuilder.DropTable(
                name: "AnalyticsResults");

            migrationBuilder.DropTable(
                name: "SentimentScorePerLabel");
        }
    }
}
