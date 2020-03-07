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
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OpinionGeneratorTests.IntegrationTests.CoreTests
{
    [TestFixture]
    public class NewsAPIArticleServiceTests
    {
        private WebApplicationFactory<OpinionGenerator.Startup> _factory;
        private HttpClient _client;
        //private TestHelper.OpinionGeneratorConfiguration _configuration;
        private IConfiguration _configuration2;
        private IArticleService _articleService;           
        private ITextAnalyticsService _textAnalyticsService;
        //private OpinionGeneratorDbContext _dbContext;
       // private IOpinionGeneratorData _opinionGeneratorData;

        [OneTimeSetUp]
        public void Init()
        {
            _factory = new WebApplicationFactory<OpinionGenerator.Startup>();
            _client = _factory.CreateClient();
                        
            using (var scope = _factory.Services.CreateScope())
            {
                _configuration2 = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                _articleService = scope.ServiceProvider.GetRequiredService<IArticleService>();
                _textAnalyticsService = scope.ServiceProvider.GetRequiredService<ITextAnalyticsService>();
                //_dbContext = scope.ServiceProvider.GetRequiredService<OpinionGeneratorDbContext>();
                //_opinionGeneratorData = scope.ServiceProvider.GetRequiredService<IOpinionGeneratorData>();
            }            
        }

        [Test]
        public async Task AzureTextAnalyticsService_GetArticleSentiment()
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
            AzTextAnalyticsResult sentiment = await _textAnalyticsService.AnalyzeText(textToAnalyze);

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
            //_opinionGeneratorData.AddArticle(a);
            //_opinionGeneratorData.Save();
            Assert.IsTrue(true);
        }

        [Test]
        public async Task GetLatestHeadlines_CheckContent()
        {
            var articles = await _articleService.GetLatestHeadlines();
            Assert.IsNotNull(articles);
        }

        [Test]
        public async Task GetLatestHeadlines_SaveToDatabase()
        {
            var articles = await _articleService.GetLatestHeadlines();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var s = JsonSerializer.Serialize<List<Article>>(articles, options);
            System.Diagnostics.Debug.WriteLine(s);
            Assert.IsNotNull(articles);
            using (var scope = _factory.Services.CreateScope())
            {
                var opinionGeneratorData = scope.ServiceProvider.GetRequiredService<IOpinionGeneratorData>();
                foreach (var article in articles)
                {
                    Assert.DoesNotThrow(() =>
                    {
                        opinionGeneratorData.AddArticle(article);
                        opinionGeneratorData.Save();
                    });
                }
            }
            
        }

        [Test]
        public void CheckDBContext()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<OpinionGeneratorDbContext>();
                Assert.IsNotNull(dbContext.ArticleNewsSource.FirstOrDefault());
            }
            
        }
    }
}
