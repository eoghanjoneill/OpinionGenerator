﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpinionGenerator.Data;

namespace OpinionGenerator.Data.Migrations
{
    [DbContext(typeof(OpinionGeneratorDbContext))]
    partial class OpinionGeneratorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OpinionGenerator.Core.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(4000)")
                        .HasMaxLength(4000);

                    b.Property<DateTimeOffset?>("PublishedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("RetrievedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("SourceIntId")
                        .HasColumnType("int");

                    b.Property<int?>("TextAnalyticsResultId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(2048)")
                        .HasMaxLength(2048);

                    b.Property<string>("UrlToImage")
                        .HasColumnType("nvarchar(2048)")
                        .HasMaxLength(2048);

                    b.HasKey("Id");

                    b.HasIndex("SourceIntId");

                    b.HasIndex("TextAnalyticsResultId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("OpinionGenerator.Core.Entities.AzTextAnalyticsResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Sentiment")
                        .HasColumnType("int");

                    b.Property<int?>("SentimentScoresId")
                        .HasColumnType("int");

                    b.Property<string>("TextAnalyzed")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SentimentScoresId");

                    b.ToTable("AzTextAnalyticsResults");
                });

            modelBuilder.Entity("OpinionGenerator.Core.Entities.NewsSource", b =>
                {
                    b.Property<int>("IntId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("IntId");

                    b.ToTable("NewsSource");
                });

            modelBuilder.Entity("OpinionGenerator.Core.Entities.SentenceSentiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AzTextAnalyticsResultId")
                        .HasColumnType("int");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<int>("Offset")
                        .HasColumnType("int");

                    b.Property<int>("Sentiment")
                        .HasColumnType("int");

                    b.Property<int?>("SentimentScoresId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AzTextAnalyticsResultId");

                    b.HasIndex("SentimentScoresId");

                    b.ToTable("SentenceSentiment");
                });

            modelBuilder.Entity("OpinionGenerator.Core.Entities.SentimentScorePerLabel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Negative")
                        .HasColumnType("float");

                    b.Property<double>("Neutral")
                        .HasColumnType("float");

                    b.Property<double>("Positive")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("SentimentScorePerLabel");
                });

            modelBuilder.Entity("OpinionGenerator.Core.Entities.Article", b =>
                {
                    b.HasOne("OpinionGenerator.Core.Entities.NewsSource", "Source")
                        .WithMany()
                        .HasForeignKey("SourceIntId");

                    b.HasOne("OpinionGenerator.Core.Entities.AzTextAnalyticsResult", "TextAnalyticsResult")
                        .WithMany()
                        .HasForeignKey("TextAnalyticsResultId");
                });

            modelBuilder.Entity("OpinionGenerator.Core.Entities.AzTextAnalyticsResult", b =>
                {
                    b.HasOne("OpinionGenerator.Core.Entities.SentimentScorePerLabel", "SentimentScores")
                        .WithMany()
                        .HasForeignKey("SentimentScoresId");
                });

            modelBuilder.Entity("OpinionGenerator.Core.Entities.SentenceSentiment", b =>
                {
                    b.HasOne("OpinionGenerator.Core.Entities.AzTextAnalyticsResult", null)
                        .WithMany("Sentences")
                        .HasForeignKey("AzTextAnalyticsResultId");

                    b.HasOne("OpinionGenerator.Core.Entities.SentimentScorePerLabel", "SentimentScores")
                        .WithMany()
                        .HasForeignKey("SentimentScoresId");
                });
#pragma warning restore 612, 618
        }
    }
}
