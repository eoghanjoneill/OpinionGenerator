using Microsoft.Extensions.Configuration;
using OpinionGenerator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using OpinionGenerator.Core.Entities;
using OpinionGenerator.Core.Services;

namespace OpinionGenerator.Services
{
    public class NewsAPIArticleServiceWrapper : IArticleService
    {
        public NewsAPIArticleServiceWrapper(IConfiguration configuration)
        {   
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            _newsAPIArticleService = new NewsAPIArticleService(configuration["NewsAPIKey"]);            
        }
        
        private readonly NewsAPIArticleService _newsAPIArticleService;

        public async Task<List<Article>> GetArticles()
        {
            return await _newsAPIArticleService.GetArticles();
        }
    }
}
