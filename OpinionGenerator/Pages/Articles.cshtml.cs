using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpinionGenerator.Core.Entities;
using OpinionGenerator.Core.Services;
using OpinionGenerator.Data;

namespace OpinionGenerator
{
    public class ArticlesModel : PageModel
    {
        [BindProperty]
        public string Output { get; set; }

        private readonly IArticleService _articleService;
        private readonly IOpinionGeneratorData _opinionGeneratorData;

        public ArticlesModel(IArticleService articleService, IOpinionGeneratorData opinionGeneratorData)
        {
            _articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
            _opinionGeneratorData = opinionGeneratorData ?? throw new ArgumentNullException(nameof(opinionGeneratorData));
        }
        public void OnGet()
        {

        }

        public async Task OnPostAsync()
        {
            var data = await _articleService.GetLatestHeadlines();
            string serialized = JsonSerializer.Serialize(data);
            Output = serialized;
        }
    }
}