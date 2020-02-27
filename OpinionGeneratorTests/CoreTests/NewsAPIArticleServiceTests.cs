using NUnit.Framework;
using OpinionGenerator.Core.Entities;
using OpinionGenerator.Core.Services;
using OpinionGeneratorTests;
using System.Text.Json;
using System.IO;
using System.Linq;
using Azure.AI.TextAnalytics;
using System.Text.Json.Serialization;

namespace OpinionGeneratorTests.CoreTests
{
    public class NewsAPIArticleServiceTests
    {        
        private TestHelper.OpinionGeneratorConfiguration _configuration;
        private NewsAPIArticleService _newsAPIArticleService;
        private AzureTextAnalyticsService _azureTextAnalyticsService;

        [OneTimeSetUp]
        public void Init()
        {
            _configuration = TestHelper.GetApplicationConfiguration(TestContext.CurrentContext.TestDirectory);
            _newsAPIArticleService = new NewsAPIArticleService(_configuration.NewsAPIKey);
            _azureTextAnalyticsService = new AzureTextAnalyticsService(_configuration.AzTextAnalytics.Url, _configuration.AzTextAnalytics.Key);
        }

        [Test]
        public void NewsAPIArticleService_GetArticles_ValidJson()
        {

        }

        [Test]
        public void AzureTextAnalyticsService_GetArticleSentiment()
        {
            var articleJson = File.ReadAllText(TestContext.CurrentContext.TestDirectory + "/CoreTests/NewsAPI-sample.json");
           
            JsonDocument doc = JsonDocument.Parse(articleJson);
            JsonElement root = doc.RootElement;
            JsonElement articles = root.GetProperty("articles");            
            foreach (JsonElement article in articles.EnumerateArray())
            {
                System.Diagnostics.Debug.WriteLine($"Article name: {article.GetProperty("title")}; description: {article.GetProperty("description")}");
            }
                        
            JsonElement article1 = articles.EnumerateArray().FirstOrDefault<JsonElement>();
            string textToAnalyze = article1.GetProperty("title") + ". " + article1.GetProperty("description");
            DocumentSentiment sentiment = _azureTextAnalyticsService.AnalyzeText(textToAnalyze).Result;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            options.Converters.Add(new JsonStringEnumConverter());
            
            Assert.IsNotNull(sentiment);
            System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(sentiment, options));

        }

    }
}
