using Microsoft.EntityFrameworkCore;
using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.Data
{
    public class OpinionGeneratorDbContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<NewsSource> ArticleNewsSource { get; set; }
        public DbSet<AzTextAnalyticsResult> AzTextAnalyticsResults { get; set; }

        public OpinionGeneratorDbContext(DbContextOptions<OpinionGeneratorDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsSource>()
                .HasIndex(n => new { n.Id, n.Name });
        }               
    }
}
