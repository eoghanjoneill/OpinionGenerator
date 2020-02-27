using Azure.AI.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.Core.Entities
{
    public class TextAnalyticsResult
    {
        public string TextAnalyzed { get; set; }

        public DocumentSentiment Sentiment { get; set; } 
    }
}
