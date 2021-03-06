﻿using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpinionGenerator.Core.Services
{
    public interface IArticleService
    {
        public Task<List<Article>> GetLatestHeadlines();

        public Task PopulateSentimentForArticles(List<Article> articles);
    }
}
