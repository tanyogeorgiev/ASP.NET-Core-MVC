using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookShop.Services.Models.Book;
using BookShop.Data;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using BookShop.Data.Models;
using BookShop.Common.Exstensions;

namespace BookShop.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly BookShopDbContext db;

        public BookService(BookShopDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BookListingServiceModel>> All(string searchText)
        => await this.db
            .Books
            .Where(b => b.Title.Contains(searchText) || b.Description.Contains(searchText))
            .OrderBy(b => b.Title)
            .Take(10)
            .ProjectTo<BookListingServiceModel>()
            .ToListAsync();

        public async Task<int> Create(
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories)
        {
            var categoriesNames = categories
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToHashSet();

            var existingCategories = await this.db
                .Categories
                .Where(c => categoriesNames.Contains(c.Name))
                .ToListAsync();

            

            var allCategories = new List<Category>(existingCategories);

            foreach (var categoryName in categoriesNames)
            {
                if (existingCategories.All(c => c.Name != categoryName))
                {
                    var category = new Category
                    {
                        Name = categoryName
                    };
                    this.db.Add(category);

                    allCategories.Add(category);
                }
            }

            await this.db.SaveChangesAsync();

            var book = new Book
            {
                Title = title,
                Description = description,
                Price = price,
                Copies = copies,
                Edition = edition,
                AgeRestriction = ageRestriction,
                ReleaseDate = releaseDate,
                AuthorId = authorId,
            };

            allCategories.ForEach(c => book.Categories.Add(new BookCategory
            {
                CategoryId = c.Id
            }));

            this.db.Add(book);
            await this.db.SaveChangesAsync();

            return book.Id;

        }

        public async Task<BookDetailsServiceModel> Details(int id)
        => await this.db
            .Books
            .Where(b => b.Id == id)
            .ProjectTo<BookDetailsServiceModel>()
            .FirstOrDefaultAsync();
    }
}
