namespace BookShop.Services
{
    using Services.Models.Author;
    using Services.Models.Book;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAuthorService
    {
        Task<AuthorDetailsServiceModel> Details(int Id);

        Task<int> Create(string FirstName, string LastName);

        Task<IEnumerable<BookByAuthorServiceModel>> BooksAsync(int id);

        Task<bool> Exists(int Id);
    }
}
