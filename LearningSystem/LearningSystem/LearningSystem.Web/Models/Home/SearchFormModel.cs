namespace LearningSystem.Web.Models.Home
{
    using LearningSystem.Service.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SearchFormModel
    {

        public string SearchText { get; set; }

        [Display(Name = "Search in Users")]
        public bool SearchInUser { get; set; } = true;

        [Display(Name = "Search in Courses")]
        public bool SearchInCourses { get; set; } = true;
    }
}
