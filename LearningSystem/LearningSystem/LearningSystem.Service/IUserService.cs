﻿namespace LearningSystem.Service
{
    using LearningSystem.Service.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string username);

        Task<IEnumerable<UserListingServiceModel>> FindAsync(string search);

        Task<byte[]> GetPdfCertificate(int courseId,string studentId);

    }
}
