
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Linq;

    public class PartsController : Controller
    {
        private readonly IPartService parts;
        private readonly ISupplierService suppliers;

        private const int PageSize = 25;


        public PartsController(IPartService parts, ISupplierService suppliers)
        {
            this.parts = parts;
            this.suppliers = suppliers;
        }

        public IActionResult Create()
            => View(new PartFormModel
            {
                SuppliersList = this.suppliers.All().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                })

            });

        [HttpPost]
        public IActionResult Create(PartFormModel modelPart)
        {
            if (!ModelState.IsValid)
            {
                return View(modelPart);
            }

            this.parts.Create(
                modelPart.Name,
                modelPart.Price,
                modelPart.Quantity,
                modelPart.SupplierId);

            return RedirectToAction(nameof(All));
        }


        public IActionResult All(int page = 1)
            => View(new PartPageListingModel
            {
                Parts = this.parts.All(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.parts.Total() / (double)PageSize)

            }
            );

        public IActionResult Edit(int Id)
        {
            var part = this.parts.ById(Id);
            var suppliersList = this.suppliers.All().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
            int supplierId = int.Parse(suppliersList
                .Where(s => s.Text == part.SupplierName)
                .Select(s => s.Value)
                .First());

            if (part == null)
            {
                return NotFound();
            }

            return View(new PartFormModel
            {
                Name = part.Name,
                Price = part.Price,
                Quantity = part.Quantity,
                SupplierId = supplierId,
                SuppliersList = suppliersList
            });

        }

        [HttpPost]
        public IActionResult Edit(int Id, PartFormModel modelPart)
        {
            if (!ModelState.IsValid)
            {
                return View(modelPart);
            }

            bool partExist = this.parts.Exists(Id);

            if (!partExist)
            {
                return NotFound();
            }

            this.parts.Edit(
                Id,
                modelPart.Name,
                modelPart.Price,
                modelPart.Quantity,
                modelPart.SupplierId
                );

            return RedirectToAction(nameof(All));
        }
    }
}

