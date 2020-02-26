using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpinionGenerator.Core.Entities;
using OpinionGenerator.Services;

namespace OpinionGenerator
{
    public class ArticlesModel : PageModel
    {
        [BindProperty]
        public string Output { get; set; }

        private readonly IArticleService _articleService;

        public ArticlesModel(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public void OnGet()
        {

        }

        public async Task OnPostAsync()
        {
            var data = await _articleService.GetArticles();
            string serialized = JsonSerializer.Serialize(data);
            Output = serialized;
        }
    }
}