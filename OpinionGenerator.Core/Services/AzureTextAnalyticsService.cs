using Azure.AI.TextAnalytics;
using System;
using System.Threading.Tasks;

namespace OpinionGenerator.Core.Services
{
    public class AzureTextAnalyticsService
    {
        private readonly string _analyticsUrl;
        private readonly string _analyticsKey;

        public AzureTextAnalyticsService(string analyticsUrl, string analyticsKey)
        {
            _analyticsUrl = analyticsUrl ?? throw new ArgumentNullException(nameof(analyticsUrl));
            _analyticsKey = analyticsKey ?? throw new ArgumentNullException(nameof(analyticsKey));
        }

        public async Task<DocumentSentiment> AnalyzeText(string textToAnalyze)
        {
            var endpoint = new Uri(_analyticsUrl);
            var key = _analyticsKey;
            var client = new TextAnalyticsClient(endpoint, new TextAnalyticsApiKeyCredential(key));
            var response = await client.AnalyzeSentimentAsync(textToAnalyze);
            return response.Value;
        }
    }
}
