

namespace CarDealer.Web.Controllers

{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Models.Cars;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    public class CarsController : Controller
    {
        private readonly ICarService cars;

        private readonly IPartService parts;


        public CarsController(ICarService cars, IPartService parts)
        {

            this.cars = cars;
            this.parts = parts;

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

        [Route("cars/parts")]
        public IActionResult Parts()=>
            View(this.cars.WithParts());

        [Route("cars/create")]
        [Authorize]
        public IActionResult Create()
            => View(new CarFormModel
            {
                PartsList = GetPartListItems()
            });

        

        [HttpPost]
        [Route("cars/create")]
        [Authorize]
        public IActionResult Create(CarFormModel modelCar)
        {

            if(!ModelState.IsValid)
            {
                modelCar.PartsList = GetPartListItems();
                return View(modelCar);
            }

            this.cars.Create(
                modelCar.Make,
                modelCar.Model,
                modelCar.TravelledDistance,
                modelCar.PartId);


            return RedirectToAction(nameof(Parts));
        }




        private IEnumerable<SelectListItem> GetPartListItems()
            => this.parts
                .AllList()
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });
    }
}
