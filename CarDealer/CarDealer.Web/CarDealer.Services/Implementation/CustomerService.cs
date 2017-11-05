﻿namespace CarDealer.Services.Implementation
{
    using Data;
    using Models;
    using Models.Customers;
    using System.Linq;
    using System.Collections.Generic;
    using System;

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
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                }).ToList();
        }

        public IEnumerable<CustomersWithSalesModel> CustomersWithSales(int Id) => this.db
            .Customers
            .Where(c => c.Id == Id)
            .Select(m => new CustomersWithSalesModel
            {
                Name = m.Name,
                BoughtCars = m.Sales.Count,
                TotalSpentMoney = m.Sales.Sum(s => s.Car.Parts.Sum(p => p.Part.Price))
            }).ToList();
    }
}
