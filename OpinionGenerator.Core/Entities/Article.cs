using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpinionGenerator.Core.Entities
{
    public class Article
    {
        #region "Properties from OpinionGenerator"
        [Key]
        public int Id { get; set; }
        public DateTimeOffset? RetrievedAt { get; set; }
        public AzTextAnalyticsResult TextAnalyticsResult { get; set; }
        #endregion

        #region "Properties from NewsAPI"
        public NewsSource Source { get; set; }
        [MaxLength(500)]
        public string Author { get; set; }
        [MaxLength(500)]
        public string Title { get; set; }
        [MaxLength(4000)]
        public string Description { get; set; }
        [MaxLength(2048)]
        public string Url { get; set; }
        [MaxLength(2048)]
        public string UrlToImage { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public string Content { get; set; }
        #endregion

        #region "Properties from query"
        [MaxLength(100)]
        public string Country { get; set; }
        #endregion

        public string TextToAnalyze
        {
            get
            {
                IEnumerable<string> text = new string[] { Title, Description, Content };
                text = text
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => AddFullStopIfRequired(s));
                return string.Join(" ", text);
            }            
        }

        private string AddFullStopIfRequired(string s)
        {
            s = s.Trim();
            if (s.EndsWith('.') || s.EndsWith('!') || s.EndsWith('?'))
            {
                return s;
            }
            else
            { return s + "."; }

        }
    }

    public class NewsSource
    {
        [Key]
        public int IntId { get; set; }
        [MaxLength(100)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
