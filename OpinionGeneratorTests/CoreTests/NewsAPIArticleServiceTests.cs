using NUnit.Framework;
using OpinionGenerator.Core.Entities;
using OpinionGenerator.Core.Services;
using OpinionGeneratorTests;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using OpinionGenerator.Data;
using Microsoft.EntityFrameworkCore;

namespace OpinionGeneratorTests.CoreTests
{
    public class NewsAPIArticleServiceTests
    {        
        private TestHelper.OpinionGeneratorConfiguration _configuration;
        private NewsAPIArticleService _newsAPIArticleService;
        private AzureTextAnalyticsService _azureTextAnalyticsService;
        private IOpinionGeneratorData _opinionGeneratorData;

        [OneTimeSetUp]
        public void Init()
        {
            _configuration = TestHelper.GetApplicationConfiguration(TestContext.CurrentContext.TestDirectory);
            _newsAPIArticleService = new NewsAPIArticleService(_configuration.NewsAPIKey);
            _azureTextAnalyticsService = new AzureTextAnalyticsService(_configuration.AzTextAnalytics.Url, _configuration.AzTextAnalytics.Key);

            var optionsBuilder = new DbContextOptionsBuilder<OpinionGeneratorDbContext>();
            optionsBuilder.UseSqlServer(_configuration.ConnectionStrings.OpinionGenerator);
            _opinionGeneratorData = new OpinionGenertorSqlData(new OpinionGeneratorDbContext(optionsBuilder.Options));
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
            Azure.AI.TextAnalytics.DocumentSentiment sentiment = _azureTextAnalyticsService.AnalyzeText(textToAnalyze).Result;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            options.Converters.Add(new JsonStringEnumConverter());
            
            Assert.IsNotNull(sentiment);
            System.Diagnostics.Debug.WriteLine(JsonSerializer.Serialize(sentiment, options));

        }

        [Test]
        public void OpinionGeneratorSqlData_AddArticles()
        {
            var articleJson = File.ReadAllText(TestContext.CurrentContext.TestDirectory + "/CoreTests/NewsAPI-sample.json");

            JsonDocument doc = JsonDocument.Parse(articleJson);
            JsonElement root = doc.RootElement;
            JsonElement articles = root.GetProperty("articles");            
            JsonElement article1 = articles.EnumerateArray().FirstOrDefault<JsonElement>();
            var a = new Article();
            a.Author = article1.GetProperty("author").GetString();
            a.Title = article1.GetProperty("title").GetString();
            a.Description = article1.GetProperty("description").GetString();
            a.Url = article1.GetProperty("url").GetString();
            a.UrlToImage = article1.GetProperty("urlToImage").GetString();
            a.PublishedAt = article1.GetProperty("publishedAt").GetDateTime();
            _opinionGeneratorData.AddArticle(a);
            _opinionGeneratorData.Save();
            Assert.IsTrue(true);
        }

    }
}
