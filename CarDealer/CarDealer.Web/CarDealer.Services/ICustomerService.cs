namespace CarDealer.Services
{
   
    using Models;
    using Models.Customers;
    using System;
    using System.Collections.Generic;

  public  interface ICustomerService
    {
        
        IEnumerable<CustomerModel> OrderedCustomers (OrderBy order);

        CustomersWithSalesModel CustomersWithSales(int Id);

        void Create(string name, DateTime birrthdate, bool isYoungDriver );

        CustomerModel ById(int id);

        void Edit(int id, string name, DateTime birthDay, bool isYoungDriver);

        bool Exists(int id);
    }
}
