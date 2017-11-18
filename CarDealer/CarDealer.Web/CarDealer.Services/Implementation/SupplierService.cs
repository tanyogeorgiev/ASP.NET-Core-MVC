using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services.Implementation
{
    using Data;
    using Services;
    using Services.Models.Suppliers;
    using System.Linq;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierModel> All()
         => this.db
                .Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name

                }).ToList();



        public IEnumerable<SupplierListingModel> AllListings(bool isImporter) => this.db
                 .Suppliers
                 .Where(s => s.IsImporter == isImporter)
                 .Select(sm => new SupplierListingModel
                 {
                     Id = sm.Id,
                     Name = sm.Name,
                     TotalParts = sm.Parts.Count
                 })
                .ToList();
    }
}
