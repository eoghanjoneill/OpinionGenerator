using Azure.AI.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OpinionGenerator.Core.Entities
{
    public class AzTextAnalyticsResult
    {
        [Key]
        public int Id { get; set; }
        
        public string TextAnalyzed { get; set; }
        
        /// <summary>
        /// Gets the predicted sentiment for the analyzed input document
        /// or substring.
        /// </summary>
        public SentimentLabel Sentiment { get; set;  }

        /// <summary>
        /// Gets the sentiment confidence score between 0 and 1,
        /// for each sentiment label.
        /// </summary>
        public SentimentScorePerLabel SentimentScores { get; set;  }

        /// <summary>
        /// Gets the predicted sentiment for each sentence in the corresponding
        /// document.
        /// </summary>
        public ICollection<SentenceSentiment> Sentences { get; set; } = new List<SentenceSentiment>();
    }

    /// <summary>
    /// The sentiment confidence scores, by sentiment class label.
    /// </summary>
    public class SentimentScorePerLabel
    {
        public int Id { get; set; }
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

    /// <summary>
    /// The predicted sentiment for a given span of text.  This may correspond
    /// to a full text document input or a substring such as a sentence of that
    /// input.  For more information regarding text sentiment, see
    /// <a href="https://docs.microsoft.com/en-us/azure/cognitive-services/Text-Analytics/how-tos/text-analytics-how-to-sentiment-analysis"/>.
    /// </summary>
    public class SentenceSentiment
    {
        public int Id { get; set; }
        /// <summary>
        /// Gets the predicted sentiment for the analyzed input document
        /// or substring.
        /// </summary>
        public SentimentLabel Sentiment { get; set; }

        /// <summary>
        /// Gets the sentiment confidence score between 0 and 1,
        /// for each sentiment label.
        /// </summary>
        public SentimentScorePerLabel SentimentScores { get; set; }

        /// <summary>
        /// Gets the start position for the matching text in the input document.
        /// The offset unit is unicode character count.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets the length of the matching text in the input document.
        /// The length unit is unicode character count.
        /// </summary>
        public int Length { get; set; }
    }

    /// <summary>
    /// The predicted sentiment label for a given span of text.
    /// </summary>
    public enum SentimentLabel
    {
        /// <summary>
        /// Indicates that the sentiment is positive.
        /// </summary>
        Positive,

        /// <summary>
        /// Indicates that the lacks a sentiment.
        /// </summary>
        Neutral,

        /// <summary>
        /// Indicates that the sentiment is negative.
        /// </summary>
        Negative,

        /// <summary>
        /// Indicates that the contains mixed sentiments.
        /// </summary>
        Mixed,
    }
}
