
namespace CarDealer.Services.Implementation
{
    using System.Collections.Generic;
    using Data;
    using System.Linq;
    using Services.Models.Parts;
    using CarDealer.Data.Models;

    public class PartService : IPartService
    {
        private readonly CarDealerDbContext db;

        public PartService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PartListingModel> All(int page, int pageSize = 10)
            => this.db
            .Parts
            .OrderByDescending(p => p.Id)
            .Select(p => new PartListingModel
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                Price = p.Price,
                SupplierName = p.Supplier.Name
            })
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .ToList();

        public void Create(string name, decimal price, int quantity, int supplierId)
        {
            var part = new Part
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                SupplierId = supplierId,
            };

            this.db.Parts.Add(part);
            this.db.SaveChanges();

        }

        public int Total() => this.db.Parts.Count();

        public PartListingModel ById(int Id)
            => this.db
            .Parts
            .Where(p => p.Id == Id)
            .Select(p => new PartListingModel
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                Price = p.Price,
                SupplierName = p.Supplier.Name
               
                
            }).FirstOrDefault();

        public bool Exists(int id) => this.db.Parts.Any(p => p.Id == id);

        public void Edit(int id, string name, decimal price, int quantity, int supplierId)
        {
            var partToEdit = this.db.Parts.Find(id);

            partToEdit.Name = name;
            partToEdit.Price = price;
            partToEdit.Quantity = quantity;
            partToEdit.SupplierId = supplierId;

            this.db.SaveChanges();
          
        }
    }
}
