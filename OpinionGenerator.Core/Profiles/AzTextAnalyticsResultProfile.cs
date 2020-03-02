using AutoMapper;
using Azure.AI.TextAnalytics;
using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.Core.Profiles
{
    public class AzTextAnalyticsResultProfile : Profile
    {
        public AzTextAnalyticsResultProfile()
        {
            CreateMap<Azure.AI.TextAnalytics.SentimentScorePerLabel, Entities.SentimentScorePerLabel>();
            CreateMap<Azure.AI.TextAnalytics.SentenceSentiment, Entities.SentenceSentiment>();
            CreateMap<DocumentSentiment, AzTextAnalyticsResult>();
        }
    }
}
