
namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using Infrastructures.Extensions;
    using LearningSystem.Data.Models;
    using LearningSystem.Service.Blog;
    using LearningSystem.Service.Html;
    using LearningSystem.Web.Areas.Blog.Models.Articles;
    using LearningSystem.Web.Infrastructures.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Area(WebConstants.BlogArea)]
    [Authorize(Roles = WebConstants.BlogAuthorRole)]
    public class ArticlesController : Controller
    {
        public readonly IBlogArticleService articles;

        public readonly IHtmlService htmlSanitizer;

        public readonly UserManager<User> userManager;

        public ArticlesController(IBlogArticleService articles, IHtmlService htmlSanitizer, UserManager<User> userManager)
        {
            this.articles = articles;
            this.htmlSanitizer = htmlSanitizer;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
            => View(new ArticleListingViewModel
            {
                Articles = await this.articles.AllAsync(page),
                TotalArticles = await this.articles.TotalAsync(),
                CurrentPage = page
            }
                );


        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(PublishArticleFormModel model)
        {
            model.Content = this.htmlSanitizer.Sanitize(model.Content);
            var userId = userManager.GetUserId(User);
            await articles.CreateAsync(
                model.Title,
                model.Content,
                userId
                );
            TempData.AddSuccessMessage($"Article {model.Title} was published successfully!");
            return RedirectToAction(nameof(Index));

        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
            => this.ViewOrNotFound(await this.articles.ById(id));
    }


}
