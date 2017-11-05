

namespace CarDealer.Web.Controllers

{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Models.Cars;

    public class CarsController : Controller
    {
        private readonly ICarService cars;

        public CarsController(ICarService cars)
        {
            this.cars = cars;

        }

        [Route("cars/{make}")]
        public IActionResult ByMake(string make)
        {
            var cars = this.cars.ByMake(make);


            return View(new CarsByMakeModel
            {
                Make = make,
                Cars = cars
            });
        }
    }
}
