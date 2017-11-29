using LearningSystem.Service.Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningSystem.Service.Blog
{
    public interface IBlogArticleService
    {
        Task<IEnumerable<BlogArticleListingServiceModel>> AllAsync(int page);

        Task<int> TotalAsync();

        Task<BlogArticleDetailsServiceModel> ById(int id);

        Task CreateAsync(string title, string content, string authorId);
    }
}
