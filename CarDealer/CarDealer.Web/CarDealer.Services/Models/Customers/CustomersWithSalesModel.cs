
namespace CarDealer.Services.Models.Customers
{
    using CarDealer.Services.Models.Sales;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomersWithSalesModel
    {
        public string Name { get; set; }

        public IEnumerable<SalesModel> BoughtCars { get; set; }
        
        public bool IsYongDriver { get; set; }

        public decimal TotalSpentMoney => BoughtCars
            .Sum(c => c.Price * (1 - (decimal)c.Discount))
            * (this.IsYongDriver ? 0.95m : 1);
    }
}



