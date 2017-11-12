
namespace CarDealer.Services.Implementation
{
    using System;
    using System.Collections.Generic;
    using CarDealer.Services.Models.Sales;
    using CarDealer.Data;
    using System.Linq;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext db;
        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleListModel> All() => this.db.Sales.Select(lm => new SaleListModel
        {
            CustomerName = lm.Customer.Name,
            Discount = lm.Discount,
            IsYoungDriver = lm.Customer.IsYoungDriver,
            Price = lm.Car.Parts.Sum(p => p.Part.Price)
        })
            .ToList();
    }
}
