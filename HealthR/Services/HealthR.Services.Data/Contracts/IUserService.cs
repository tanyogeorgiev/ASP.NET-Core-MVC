using HealthR.Services.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthR.Services.Data.Contracts
{
    public interface IUserService
    {

        Task CreateSchedule(string userId, string name);
        Task<UserProfileServiceModel> ProfileAsync(string id);
        
    }
}
