

namespace CarDealer.Web.Controllers
{
    using Services;
    using Microsoft.AspNetCore.Mvc;

    public class SalesController : Controller
    {
        private readonly ISaleService sales;

        public SalesController(ISaleService sales)
        {
            this.sales = sales;
        }

        [Route("sales")]
        public IActionResult All() => View(sales.All());
    }
}
