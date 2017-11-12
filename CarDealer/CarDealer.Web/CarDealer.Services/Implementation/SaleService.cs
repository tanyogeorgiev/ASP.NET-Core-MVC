
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
            Id = lm.Id,
            CustomerName = lm.Customer.Name,
            Discount = lm.Discount,
            IsYoungDriver = lm.Customer.IsYoungDriver,
            Price = lm.Car.Parts.Sum(p => p.Part.Price)
        })
            .ToList();

        public SaleDetailsModel Details(int id) =>
            this.db
            .Sales
            .Where(s => s.Id == id)
            .Select(m => new SaleDetailsModel
            {
                Id = m.Id,
                CustomerName = m.Customer.Name,
                Discount = m.Discount,
                IsYoungDriver = m.Customer.IsYoungDriver,
                Price = m.Car.Parts.Sum(p => p.Part.Price),
                Car = new Models.Cars.CarModel
                {
                    Make = m.Car.Make,
                    Model = m.Car.Model,
                    TravelledDistance = m.Car.TravelledDistance
                }

            }).FirstOrDefault();
    }
}
