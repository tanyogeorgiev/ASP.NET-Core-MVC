
namespace CarDealer.Services.Implementation

{
    using Data.Models;
    using Models;
    using Data;
    using Models.Cars;
    using System.Collections.Generic;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public void Create(string make, string model, long travelledDistance, int[] partsList)
        {
            var existingPartIds = this.db.Parts.Where(p => partsList.Contains(p.Id)).Select(p => p.Id).ToList();
            var newCar = new Car
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance,
            };
            this.db.Cars.Add(newCar);

            newCar.Parts.AddRange(existingPartIds.Select(partId => new PartCar
            {
             PartId = partId

            }));



         
            this.db.SaveChanges();
        }

        public IEnumerable<CarModel> ByMake(string make)
        {
            return this.db
                .Cars
                .Where(c => c.Make.ToLower() == make.ToLower())
                .OrderBy(c => c.Model)
                .ThenBy(c => c.TravelledDistance)
                .Select(c =>
                new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                }).ToList();

        }



        public IEnumerable<CarWithPartsModel> WithParts() => this.db
                .Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts.Select(p => new PartModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                }).ToList();


    }
}