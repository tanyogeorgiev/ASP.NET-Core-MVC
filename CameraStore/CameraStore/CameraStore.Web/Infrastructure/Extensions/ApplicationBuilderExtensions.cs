
namespace CameraStore.Web.Infrastructure.Extensions
{
    using CameraStore.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                 serviceScope.ServiceProvider.GetService<CameraStoreDbContext>().Database.Migrate();
            }
            return app;

        }
    }
}