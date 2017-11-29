using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Service.Html;
using System;
using System.Threading.Tasks;
using LearningSystem.Service.Blog.Models;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace LearningSystem.Service.Blog.Implementations
{
    public  class BlogArticleService : IBlogArticleService
    {
        public readonly LearningSystemDbContext db;

      

        public BlogArticleService(LearningSystemDbContext db)
        {
            this.db = db;
        
        }

        public async Task<IEnumerable<BlogArticleListingServiceModel>> AllAsync(int page)
            => await this.db
            .Articles
            .OrderByDescending(a => a.PublishDate)
            .Skip((page - 1) * ServiceConstants.BlogArticlesPageSize)
            .Take(ServiceConstants.BlogArticlesPageSize)
            .ProjectTo<BlogArticleListingServiceModel>()
            .ToListAsync();

        public async Task<BlogArticleDetailsServiceModel> ById(int id)
            => await this.db
            .Articles
            .Where(a => a.Id == id)
            .ProjectTo<BlogArticleDetailsServiceModel>()
            .FirstOrDefaultAsync();

        public async Task CreateAsync(string title, string content, string authorId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow,
                AuthorId = authorId
            };

            this.db.Add(article);

            await this.db.SaveChangesAsync();
        }

        public async Task<int> TotalAsync()
            => await this.db.Articles.CountAsync();
    }
}
