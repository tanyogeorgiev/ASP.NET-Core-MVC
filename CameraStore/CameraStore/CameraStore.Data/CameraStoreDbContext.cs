

namespace CameraStore.Data
{
    using Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class CameraStoreDbContext : IdentityDbContext<User>
    {
        public CameraStoreDbContext(DbContextOptions<CameraStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Camera> Cameras { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
               .Entity<User>()
               .HasMany(u => u.Cameras)
               .WithOne(c => c.User)
               .HasForeignKey(fk => fk.UserId);

            base.OnModelCreating(builder);
          
        }
    }
}
