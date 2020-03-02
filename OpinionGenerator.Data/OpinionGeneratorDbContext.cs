﻿using Microsoft.EntityFrameworkCore;
using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.Data
{
    public class OpinionGeneratorDbContext : DbContext
    {
        public OpinionGeneratorDbContext(DbContextOptions<OpinionGeneratorDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<AzTextAnalyticsResult> AzTextAnalyticsResults { get; set; }
               
    }
}
