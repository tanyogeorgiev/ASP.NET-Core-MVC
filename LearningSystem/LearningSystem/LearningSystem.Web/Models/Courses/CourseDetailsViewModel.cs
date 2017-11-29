
namespace LearningSystem.Web.Models.Courses
{
    using LearningSystem.Service.Models;

    public class CourseDetailsViewModel
    {
        public CourseDetailsServiceModel Course { get; set; }

        public bool UserIsSignedInCourse { get; set; }
    }
}
