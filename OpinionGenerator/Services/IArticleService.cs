using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpinionGenerator.Services
{
    public interface IArticleService
    {
        public Task<List<Article>> GetArticles();
    }
}
