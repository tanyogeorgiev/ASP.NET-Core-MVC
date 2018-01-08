

namespace BookShop.Api.Controllers
{
    using Infrastructure.Exstensions;
    using Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Models.Books;
    using BookShop.Api.Infrastructure.Filters;

    public class BooksController : BaseController
    {
        private readonly IBookService books;
        private readonly IAuthorService authors;

        public BooksController(IBookService books, IAuthorService authors)
        {
            this.books = books;
            this.authors = authors;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string search = "")
            => this.Ok(await this.books.All(search));

        [HttpGet(WebConstants.WithId)]
        public async Task<IActionResult> Get(int id)
        => this.OkOrNotFound(await this.books.Details(id));

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] CreateBookRequestModel model)
        {
            if (!await this.authors.Exists(model.AuthorId))
            {
                return BadRequest("Author does not exist");
            }

            return Ok(await this.books.Create(
            model.Title,
            model.Description,
            model.Price,
            model.Copies,
            model.Edition,
            model.AgeRestriction,
            model.ReleaseDate,
            model.AuthorId,
            model.Categories));

           
        }
    }
}
