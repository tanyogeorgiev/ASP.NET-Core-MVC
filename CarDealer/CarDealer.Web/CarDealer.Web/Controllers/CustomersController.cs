namespace CarDealer.Web.Controllers
{
    using Services.Models;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Models.Customers;
    using CarDealer.Web.Infrastructures.Extensions;


    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("all/{order}")]
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


        [Route("{id}")]
        public IActionResult TotalSales(int id) =>
             this.ViewOrNotFound(this.customers.CustomersWithSales(id));

        [Route(nameof(Create))]
        public IActionResult Create() => View();


        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CustomerFormModel customerModel)
        {

            if (!ModelState.IsValid)
            {
                return View(customerModel);
            }

            this.customers.Create(customerModel.Name,
                                  customerModel.BirthDay,
                                  customerModel.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderBy.Ascending });
        }

        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = this.customers.ById(id);
            
            if(customer ==null)
            {
                return NotFound();
            }

            return View(new CustomerFormModel()
            {
                Name = customer.Name,
                BirthDay = customer.BirthDate,
                IsYoungDriver = customer.IsYoungDriver
            });
        }



        [HttpPost]
        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id, CustomerFormModel modelC )
        {

            if (!ModelState.IsValid)
            {
                return View(modelC);
            }

            bool customerExists = this.customers.Exists(id);

           if (!customerExists)
            {
                return NotFound();
            }

             
               this.customers.Edit(
                id,
                modelC.Name,
                modelC.BirthDay,
                modelC.IsYoungDriver
            );

            return RedirectToAction(nameof(All), new { order = OrderBy.Ascending });
        }

    }
}
