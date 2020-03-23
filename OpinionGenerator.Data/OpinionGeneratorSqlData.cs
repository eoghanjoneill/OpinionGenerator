using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpinionGenerator.Data
{
    public class OpinionGeneratorSqlData : IOpinionGeneratorData, IDisposable
    {
        private OpinionGeneratorDbContext _context;

        public OpinionGeneratorSqlData(OpinionGeneratorDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Article> GetArticles()
        {
            return _context.Articles;
        }

        public void AddArticle(Article article)
        {
            if (article.Source != null)
            {
                var newsSource = _context.ArticleNewsSource.FirstOrDefault(s => 
                    s.Id == article.Source.Id && s.Name == article.Source.Name);
                article.Source = newsSource ?? article.Source;
            }
                       
            _context.Articles.Add(article);
        }

        public void UpdateArticle(Article article)
        {
            if (article.Source != null && article.Source.IntId == 0)
            {
                var newsSource = _context.ArticleNewsSource.FirstOrDefault(s =>
                    s.Id == article.Source.Id && s.Name == article.Source.Name);
                article.Source = newsSource ?? article.Source;
            }
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }       
    }
}
