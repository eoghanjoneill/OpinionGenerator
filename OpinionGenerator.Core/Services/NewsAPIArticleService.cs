using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using OpinionGenerator.Core.Entities;

namespace OpinionGenerator.Core.Services
{
    public class NewsAPIArticleService
    {
        public NewsAPIArticleService(string apiKey)
        {
            _url = new Uri("http://newsapi.org/v2/top-headlines?country=gb");
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            _client.DefaultRequestHeaders.Add("x-api-key", _apiKey);
        }

        private readonly Uri _url;
        private readonly string _apiKey;
        private readonly HttpClient _client;

        public async Task<List<Article>> GetArticles()
        {
            var response = await _client.GetAsync(_url);
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                var articles = await JsonSerializer.DeserializeAsync<List<Article>>(stream);
                return articles;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Status code: {(int)response.StatusCode}; Reason: {response.ReasonPhrase}");
            }
            return null;
        }
    }
}
