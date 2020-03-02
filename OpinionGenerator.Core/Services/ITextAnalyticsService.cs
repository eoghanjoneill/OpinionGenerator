using OpinionGenerator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpinionGenerator.Core.Services
{
    public interface ITextAnalyticsService
    {
        public Task<AzTextAnalyticsResult> AnalyzeText(string textToAnalyze);
    }
}
