namespace BookShop.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using BookShop.Data;
    using BookShop.Data.Models;
    using BookShop.Services.Models.Book;
    using BookShop.Services.Models.Author;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class AuthorService : IAuthorService
    {
        private readonly BookShopDbContext db;

        public AuthorService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BookByAuthorServiceModel>> BooksAsync(int id)
        => await this.db
            .Books
            .Where(b => b.AuthorId == id)
            .ProjectTo<BookByAuthorServiceModel>()
            .ToListAsync();


        public async Task<int> Create (string FirstName, string LastName)
        {
            var author = new Author
            {
                FirstName = FirstName,
                LastName = LastName
            };

            this.db.Add(author);
            await this.db.SaveChangesAsync();

            return author.Id;

        }

        public async Task<AuthorDetailsServiceModel> Details(int Id)
        => await this.db
            .Authors
            .Where(a => a.Id == Id)
            .ProjectTo<AuthorDetailsServiceModel>()
            .FirstOrDefaultAsync();

        public async Task<bool> Exists(int Id)
        => await this.db
            .Authors.AnyAsync(a => a.Id == Id);

    }
}
