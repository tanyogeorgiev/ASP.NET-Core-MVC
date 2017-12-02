using LearningSystem.Service.Models;
using System.Collections.Generic;

namespace LearningSystem.Web.Models.Trainer
{
    public class StudentInCourseViewModel
    {
        public IEnumerable<StudentInCourseServiceModel> Students { get; set; }

        public  CourseListingServiceModel Course { get; set; }
    }
}
