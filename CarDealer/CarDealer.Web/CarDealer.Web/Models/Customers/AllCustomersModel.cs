

namespace CarDealer.Web.Models.Customers
{
    using Services.Models;
    using Services.Models.Customers;
    using System.Collections.Generic;

    public class AllCustomersModels

    {
        public IEnumerable<CustomerModel> Customers { get; set; }

        public OrderBy OrderBy { get; set; }
    }
}
