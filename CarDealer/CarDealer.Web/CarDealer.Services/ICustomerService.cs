﻿namespace CarDealer.Services
{
   
    using Models;
    using Models.Customers;
    using System.Collections.Generic;

  public  interface ICustomerService
    {
        
        IEnumerable<CustomerModel> OrderedCustomers (OrderBy order);

        CustomersWithSalesModel CustomersWithSales(int Id);

    }
}
