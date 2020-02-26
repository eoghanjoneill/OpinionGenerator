using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpinionGenerator.Core.Entities
{
    public class Article
    {
        #region "Properties from OpinionGenerator"
        public int Id { get; set; }
        public DateTime? RetrievedAt { get; set; }
        //public Azure MyProperty { get; set; }
        #endregion

        #region "Properties from NewsAPI"
        public NewsSource Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime? PublishedAt { get; set; }
        #endregion
    }

    public class NewsSource
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
