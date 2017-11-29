
namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    using LearningSystem.Service.Blog.Models;
    using System;
    using System.Collections.Generic;
    using Service;


    public class ArticleListingViewModel
    {
        public IEnumerable<BlogArticleListingServiceModel> Articles { get; set; }

        public int TotalArticles { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((this.TotalArticles * 1.00) / ServiceConstants.BlogArticlesPageSize);

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage ==  TotalPages ? this.CurrentPage : this.CurrentPage + 1;
    }
}
