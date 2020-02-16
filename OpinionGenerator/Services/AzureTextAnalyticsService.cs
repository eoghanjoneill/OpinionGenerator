using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpinionGenerator.Services
{
    public class AzureTextAnalyticsService : ITextAnalyticsService
    {
        private readonly IConfiguration _configuration;

        public AzureTextAnalyticsService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<AnalyzeSentimentResult> AnalyzeText(string textToAnalyze)
        {
            var endpoint = new Uri(_configuration["AzTextAnalytics:Url"]);
            var key = _configuration["AzTextAnalytics:Key"];
            var client = new TextAnalyticsClient(endpoint, key);
            var response = await client.AnalyzeSentimentAsync(textToAnalyze);
            return response.Value;
        }
    }
}
