﻿
using LearningSystem.Common.Mapping;
using LearningSystem.Data.Models;
using System;
using AutoMapper;

namespace LearningSystem.Service.Blog.Models
{
    public class BlogArticleListingServiceModel : IMapFrom<Article>, IHaveCustomMapping
    {
        public int Id { get; set; }
                
        public string Title { get; set; }
            
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public User Author { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Article, BlogArticleListingServiceModel>()
            .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
    }
}
