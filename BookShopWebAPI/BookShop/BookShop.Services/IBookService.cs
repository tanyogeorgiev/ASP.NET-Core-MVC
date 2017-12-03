using BookShop.Services.Models.Book;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShop.Services
{
    
    public interface IBookService
    {
        Task<int> Create(
            string title,
            string description,
            decimal price,
            int copies,
            int? edition,
            int? ageRestriction,
            DateTime releaseDate,
            int authorId,
            string categories);
        
        Task<BookDetailsServiceModel> Details(int id);

        Task<IEnumerable<BookListingServiceModel>> All(string searchText);
    }
}
