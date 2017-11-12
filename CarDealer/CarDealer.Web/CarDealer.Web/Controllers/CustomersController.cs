namespace CarDealer.Web.Controllers
{
    using Services.Models;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Models.Customers;
    using CarDealer.Web.Infrastructures.Extensions;

    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("customers/all/{order}")]
        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "descending"
                 ? OrderBy.Descending
                 : OrderBy.Ascending;

            var customers = this.customers.OrderedCustomers(orderDirection);

            return View(new AllCustomersModels
            {
                Customers = customers,
                OrderBy = orderDirection
            });
        }

        [Route("customers/{id}")]
        public IActionResult ById(int Id) =>
            View(this.customers.CustomersWithSales(Id));


        [Route("{id}")]
        public IActionResult TotalSales(int id) =>
             this.ViewOrNotFound(this.customers.CustomersWithSales(id));
        
    }
}
 