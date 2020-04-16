using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpinionGenerator.Core.Entities;
using OpinionGenerator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpinionGenerator.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class ArticlesController : ControllerBase
    {
        private readonly IOpinionGeneratorData _opinionGeneratorData;
        private readonly IMapper _mapper;

        public ArticlesController(IOpinionGeneratorData opinionGeneratorData, IMapper mapper)
        {
            _opinionGeneratorData = opinionGeneratorData ?? throw new ArgumentNullException(nameof(opinionGeneratorData));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<Article>> GetArticles()
        {
            var articlesFromRepo = _opinionGeneratorData.GetArticles();
            return Ok(articlesFromRepo);
        }

        [HttpGet("{articleId}", Name = nameof(GetArticle))]
        [HttpHead]
        public ActionResult<IEnumerable<Article>> GetArticle(int articleId)
        {
            var article = _opinionGeneratorData.GetArticle(articleId);
            return Ok(article);
        }
    }
}
