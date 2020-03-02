using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.Core.Models
{
    public class ArticleDto
    {
        #region "Properties from NewsAPI"
        public NewsSourceDto Source { get; set; }
        
        public string Author { get; set; }
        
        public string Title { get; set; }
       
        public string Description { get; set; }
        
        public string Url { get; set; }
        
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
        #endregion
    }

    public class NewsSourceDto
    {
        public string Id { get; set; }        
        public string Name { get; set; }
    }
}
