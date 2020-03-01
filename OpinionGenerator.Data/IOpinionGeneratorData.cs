using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.Data
{
    public interface IOpinionGeneratorData
    {
        
        IEnumerable<Article> GetArticles();

        void AddArticle(Article article);

        bool Save();
               
    }
}
