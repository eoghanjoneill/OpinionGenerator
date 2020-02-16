using Azure.AI.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpinionGenerator.Services
{
    public interface ITextAnalyticsService
    {
        public Task<AnalyzeSentimentResult> AnalyzeText(string textToAnalyze);
    }
}
