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
    }
}
