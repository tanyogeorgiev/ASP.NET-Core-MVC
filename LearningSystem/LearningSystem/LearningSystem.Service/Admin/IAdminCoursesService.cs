using System;
using System.Threading.Tasks;

namespace LearningSystem.Service.Admin
{
    public interface IAdminCoursesService
    {
         Task  Create(
             string name,
             string description,
             DateTime startDate,
             DateTime endDate,
             string trainerId) ;
    }
}
