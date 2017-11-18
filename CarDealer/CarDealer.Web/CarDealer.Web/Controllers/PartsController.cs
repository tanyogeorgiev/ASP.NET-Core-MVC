
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
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
                SuppliersList = GetSupplierListItems()

            });

        [HttpPost]
        public IActionResult Create(PartFormModel modelPart)
        {
            //ModelState.AddModelError(nameof(PartFormModel.SupplierId), "Invalid supplier.");


            if (!ModelState.IsValid)
            {
                modelPart.SuppliersList = GetSupplierListItems();
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
            var supplierId = part.SupplierId;

            var suppliersList = GetSupplierListItems(supplierId);
              


            if (part == null)
            {
                return NotFound();
            }

            return View(new PartFormModel
            {
                Name = part.Name,
                Price = part.Price,
                Quantity = part.Quantity,
                SupplierId = part.SupplierId,
                SuppliersList = suppliersList
            });

        }

        [HttpPost]
        public IActionResult Edit(int Id, PartFormModel modelPart)
        {
            if (!ModelState.IsValid)
            {
              
                modelPart.SuppliersList = GetSupplierListItems(modelPart.SupplierId);
                return View(modelPart);
            }

            bool partExist = this.parts.Exists(Id);

            if (!partExist)
            {
                return NotFound();
            }

            this.parts.Edit(
                Id,               
                modelPart.Price,
                modelPart.Quantity               
                );

            return RedirectToAction(nameof(All));
        }


        public IActionResult Delete(int Id)
        {
            var part = this.parts.ById(Id);

            int supplierId = part.SupplierId;

            var suppliersList = GetSupplierListItems(supplierId);


            if (part == null)
            {
                return NotFound();
            }

            return View(new DeleteFormModel
            {
                Id = Id,
                Name = part.Name,
                Price = part.Price,
                Quantity = part.Quantity,
                SupplierId = part.SupplierId,
                SuppliersList = suppliersList
            });
        }

        
        public IActionResult Destroy(int id)
        {
            bool partExist = this.parts.Exists(id);

            if (!partExist)
            {
                return NotFound();
            }

            this.parts.Delete(id);

            return RedirectToAction(nameof(All));
        }


        private IEnumerable<SelectListItem> GetSupplierListItems(int id = 0)

        {
          var supplierList =  this.suppliers.All().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });

            if (id != 0)
            {
                supplierList = supplierList.Where(s => s.Value == id.ToString());
            }


            return supplierList;
        }



    }
}

