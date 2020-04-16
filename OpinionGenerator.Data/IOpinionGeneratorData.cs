using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.Data
{
    public interface IOpinionGeneratorData
    {
        
        IEnumerable<Article> GetArticles();
        Article GetArticle(int articleId);

        void AddArticle(Article article);
        void UpdateArticle(Article article);
        bool Save();
        
       
    }
}
