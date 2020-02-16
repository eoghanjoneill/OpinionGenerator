using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OpinionGenerator.Services;

namespace OpinionGenerator
{
    public class TextAnalyticsModel : PageModel
    {
        [BindProperty]
        public string TextToAnalyze { get; set; }
        [BindProperty]
        public string Output { get; set; }

        private readonly ITextAnalyticsService _analyticsService;

        public TextAnalyticsModel(ITextAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public void OnGet()
        {
            TextToAnalyze = "It was the best of times, it was the worst of times, it was the age of wisdom, it was the age of foolishness, it was the epoch of belief, it was the epoch of incredulity, it was the season of Light, it was the season of Darkness, it was the spring of hope, it was the winter of despair, we had everything before us, we had nothing before us, we were all going direct to heaven, we were all going direct the other way - in short, the period was so far like the present period, that some of its noisiest authorities insisted on its being received, for good or for evil, in the superlative degree of comparison only.";
        }

        public async Task OnPostAsync()
        {
            var response = await _analyticsService.AnalyzeText(TextToAnalyze);
            Output = JsonSerializer.Serialize(response);
        }

    }
}