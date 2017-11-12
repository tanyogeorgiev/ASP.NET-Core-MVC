namespace CarDealer.Services.Implementation
{
    using Data;
    using Models;
    using Models.Customers;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using Models.Sales;

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

        public CustomersWithSalesModel CustomersWithSales(int Id) => this.db
            .Customers
            .Where(c => c.Id == Id)
            .Select(m => new CustomersWithSalesModel
            {
                Name = m.Name,
                IsYongDriver = m.IsYoungDriver,
                BoughtCars = m.Sales.Select(s=> new SalesModel{
                    
                    Price = s.Car.Parts.Sum(p => p.Part.Price),
                    Discount = s.Discount
              
                })
            })
            .FirstOrDefault();


    }
}
