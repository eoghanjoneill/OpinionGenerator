using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpinionGenerator.Core.Profiles
{
    public class ArticlesProfile : Profile
    {
        public ArticlesProfile()
        {            
            CreateMap<Models.ArticleDto, Entities.Article>();
        }
    }    
}
