
namespace BookShop.Api.Models.Authors
{
    using BookShop.Data;
    using System.ComponentModel.DataAnnotations;

    public class AuthorRequestModel
    {
        [Required]
        [MinLength(DataConstants.AuthorNameMinLength)]
        [MaxLength(DataConstants.AuthorNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(DataConstants.AuthorNameMinLength)]
        [MaxLength(DataConstants.AuthorNameMaxLength)]
        public string LastName { get; set; }
    }
}
