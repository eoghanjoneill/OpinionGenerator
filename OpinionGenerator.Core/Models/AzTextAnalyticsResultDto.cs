using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.Core.Models
{
    public class AzTextAnalyticsResultDto
    {
        public int Id { get; set; }
        public string TextAnalyzed { get; set; }        
        public SentimentLabel Sentiment { get; set; }        
        public SentimentScorePerLabelDto SentimentScores { get; set; }
    }

    public class SentimentScorePerLabelDto
    {
        /// <summary>
        /// Gets a score between 0 and 1, indicating the confidence that the
        /// sentiment of the analyzed text is positive.
        /// </summary>
        public double Positive { get; set; }

        /// <summary>
        /// Gets a score between 0 and 1, indicating the confidence that the
        /// sentiment of the analyzed text is neutral.
        /// </summary>
        public double Neutral { get; set; }

        /// <summary>
        /// Gets a score between 0 and 1, indicating the confidence that the
        /// sentiment of the analyzed text is negative.
        /// </summary>
        public double Negative { get; set; }

    }

    public class SentenceSentimentDto
    {                
        public SentimentLabel Sentiment { get; set; }
        public SentimentScorePerLabel SentimentScores { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }
    }
}
