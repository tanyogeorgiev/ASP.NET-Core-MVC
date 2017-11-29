using LearningSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Areas.Blog.Models.Articles
{
    public class PublishArticleFormModel
    {
        [Required]
        [MinLength(DataConstants.ArticleTitleMinLength)]
        [MaxLength(DataConstants.ArticleTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

    
    }
}
