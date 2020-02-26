using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;
using OpinionGenerator.Core.Services;
using System;
using System.Threading.Tasks;

namespace OpinionGenerator.Services
{
    public class AzureTextAnalyticsServiceWrapper : ITextAnalyticsService
    {        
        private readonly AzureTextAnalyticsService _azureTextAnalyticsService;
        private readonly IConfiguration _configuration;

        public AzureTextAnalyticsServiceWrapper(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var endpoint = _configuration["AzTextAnalytics:Url"];
            var key = _configuration["AzTextAnalytics:Key"];
            _azureTextAnalyticsService = new AzureTextAnalyticsService(endpoint, key);
            
        }

        public async Task<DocumentSentiment> AnalyzeText(string textToAnalyze)
        {
            return await _azureTextAnalyticsService.AnalyzeText(textToAnalyze);            
        }
    }
}
