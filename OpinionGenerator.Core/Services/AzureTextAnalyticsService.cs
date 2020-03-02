using AutoMapper;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Options;
using OpinionGenerator.Core.Entities;
using System;
using System.Threading.Tasks;

namespace OpinionGenerator.Core.Services
{
    public class AzureTextAnalyticsService : ITextAnalyticsService
    {
        private readonly string _analyticsUrl;
        private readonly string _analyticsKey;
        private readonly IMapper _mapper;

        public AzureTextAnalyticsService(IOptions<AzureTextAnalyticsOptions> options, IMapper mapper)//string analyticsUrl, string analyticsKey)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }
            _mapper = mapper;
            _analyticsUrl = options.Value.Url;
            _analyticsKey = options.Value.Key;
        }

        public async Task<AzTextAnalyticsResult> AnalyzeText(string textToAnalyze)
        {
            var endpoint = new Uri(_analyticsUrl);
            var key = _analyticsKey;
            var client = new TextAnalyticsClient(endpoint, new TextAnalyticsApiKeyCredential(key));
            var response = await client.AnalyzeSentimentAsync(textToAnalyze);
            var textAnalyticsResult = _mapper.Map<DocumentSentiment, AzTextAnalyticsResult>(response.Value);
            textAnalyticsResult.TextAnalyzed = textToAnalyze;
            
            return textAnalyticsResult;
        }
    }
}
