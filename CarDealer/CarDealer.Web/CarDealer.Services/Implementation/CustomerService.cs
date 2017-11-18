namespace CarDealer.Services.Implementation
{
    using Data;
    using Models;
    using Models.Customers;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using Models.Sales;
    using Data.Models;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderBy order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderBy.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.Name);
                    break;
                case OrderBy.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.Name);

                    break;
                default:
                    throw new InvalidOperationException($"Invalid order by: {order}.");
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                }).ToList();
        }

        public CustomersWithSalesModel CustomersWithSales(int Id) => this.db
            .Customers
            .Where(c => c.Id == Id)
            .Select(m => new CustomersWithSalesModel
            {
                Name = m.Name,
                IsYongDriver = m.IsYoungDriver,
                BoughtCars = m.Sales.Select(s => new SalesModel
                {

                    Price = s.Car.Parts.Sum(p => p.Part.Price),
                    Discount = s.Discount

                })
            })
            .FirstOrDefault();

        public void Create(string name, DateTime birthDate, bool isYoundgDriver)
        {
            var customer = new Customer()
            {

                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = isYoundgDriver
            };

            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public CustomerModel ById(int id)
            => this.db
            .Customers
            .Where(c => c.Id == id)
            .Select(c => new CustomerModel
            {
                Id = c.Id,
                Name = c.Name,
                BirthDate = c.BirthDate,
                IsYoungDriver = c.IsYoungDriver
            })
            .FirstOrDefault();

        public void Edit(int id, string name, DateTime birthDay, bool isYoungDriver)
        {
            var currentCustomer = this.db.Customers.Find(id);

            currentCustomer.Name = name;
            currentCustomer.BirthDate = birthDay;
            currentCustomer.IsYoungDriver = isYoungDriver;

            this.db.SaveChanges();

        }

        public bool Exists(int id) => this.db.Customers.Any(c => c.Id == id);
    }
}

