using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.Core.Models
{
    public class ArticleDto
    {
        #region "Properties from OpinionGenerator"        
        public int Id { get; set; }
        public DateTimeOffset? RetrievedAt { get; set; }
        public AzTextAnalyticsResultDto TextAnalyticsResult { get; set; }
        #endregion
        #region "Properties from NewsAPI"
        public string NewsSourceId { get; set; }
        public string NewsSourceName { get; set; }
        public string Author { get; set; }        
        public string Title { get; set; }       
        public string Description { get; set; }        
        public string Url { get; set; }        
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string Content { get; set; }
        #endregion
    }    
}
