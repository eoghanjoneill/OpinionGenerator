using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using OpinionGenerator.Core.Models;
using OpinionGenerator.Core.Entities;
using Microsoft.Extensions.Options;
using AutoMapper;

namespace OpinionGenerator.Core.Services
{
    public class NewsAPIArticleService : IArticleService
    {
        public NewsAPIArticleService(IOptions<NewsAPIOptions> newsAPIOptions, IMapper mapper, ITextAnalyticsService analyticsService)
        {
            if (newsAPIOptions == null) throw new ArgumentNullException(nameof(newsAPIOptions));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _analyticsService = analyticsService ?? throw new ArgumentNullException(nameof(analyticsService));

            var baseUrl = newsAPIOptions.Value.BaseUrl;
            if (!baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl.TrimEnd('/');
            }
            _baseUrl = baseUrl;            
            _apiKey = newsAPIOptions.Value.APIKey;
            
            _client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            _client.DefaultRequestHeaders.Add("x-api-key", _apiKey);
        }

        private readonly string _baseUrl;
        private readonly string _apiKey;
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        private readonly ITextAnalyticsService _analyticsService;

        public async Task<List<Article>> GetLatestHeadlines()
        {
            var country = "gb";
            var url = new Uri($"{_baseUrl}/top-headlines?country={country}");

            var response = await _client.GetAsync(url);            
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                
                var docOptions = new JsonDocumentOptions
                {
                    AllowTrailingCommas = true
                };
                var document = await JsonDocument.ParseAsync(stream, docOptions);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var articleDtos = JsonSerializer.Deserialize<List<ArticleDto>>(document.RootElement.GetProperty("articles").GetRawText(), options);

                var retrievalDate = DateTimeOffset.UtcNow;

                var articleEntities = new List<Article>();
                foreach (ArticleDto articleDto in articleDtos)
                {
                    var articleEntity = _mapper.Map<ArticleDto, Article>(articleDto);
                    articleEntity.RetrievedAt = retrievalDate;
                    articleEntity.Country = country;
                    articleEntities.Add(articleEntity);
                }
                return articleEntities;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Status code: {(int)response.StatusCode}; Reason: {response.ReasonPhrase}");
            }
            return null;
        }

        public async Task PopulateSentimentForArticles(List<Article> articles)
        {
            if (articles == null) throw new NotImplementedException(nameof(articles));
            foreach (Article article in articles)
            {
                if (article.TextAnalyticsResult == null)
                {
                    await PopulateSentimentForArticle(article);
                }
            }
        }

        private async Task PopulateSentimentForArticle(Article article)
        {
            var result = await _analyticsService.AnalyzeText(article.TextToAnalyze);
            article.TextAnalyticsResult = result;
            
        }
    }
}
