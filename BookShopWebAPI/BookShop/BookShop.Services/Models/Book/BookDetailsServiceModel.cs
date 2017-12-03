

namespace BookShop.Services.Models.Book
{
    using AutoMapper;
    using BookShop.Common.Mapping;
    using Data.Models;
    using System.Linq;

    public class BookDetailsServiceModel : BookByAuthorServiceModel, IMapFrom<Book>
    {
        public string Author { get; set; }

        public override void ConfigureMapping(Profile mapper)

        => mapper
            .CreateMap<Book, BookDetailsServiceModel>()
            .ForMember(b => b.Categories, cfg =>
              cfg.MapFrom(b => b.Categories.Select(c => c.Category.Name)))
            .ForMember(b => b.Author, cfg =>
              cfg.MapFrom(b => b.Author.FirstName+" "+b.Author.LastName));
    }
}
