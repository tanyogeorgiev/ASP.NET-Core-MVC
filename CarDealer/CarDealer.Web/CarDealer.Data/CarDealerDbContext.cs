
namespace CarDealer.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using CarDealer.Data.Models;

    public class CarDealerDbContext : IdentityDbContext<User>
    {
        public CarDealerDbContext(DbContextOptions<CarDealerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Sale>()
                .HasOne(c => c.Car)
                .WithMany(s => s.Sales)
                .HasForeignKey(c => c.CarId);

            builder
                .Entity<Sale>()
                .HasOne(c => c.Customer)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.CustomerId);

            builder
                .Entity<Supplier>()
                .HasMany(s => s.Parts)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);

            builder
                .Entity<PartCar>()
                .HasKey(pc => new { pc.CarId, pc.PartId });

            builder
                .Entity<PartCar>()
                .HasOne(pc => pc.Part)
                .WithMany(p => p.Cars)
                .HasForeignKey(pc => pc.PartId);

            builder
                .Entity<PartCar>()
                .HasOne(pc => pc.Car)
                .WithMany(c => c.Parts)
                .HasForeignKey(pc => pc.CarId);

            base.OnModelCreating(builder);

        }
    }
}
