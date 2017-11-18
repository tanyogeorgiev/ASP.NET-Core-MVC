namespace CarDealer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Models.Suppliers;

    public class SuppliersController : Controller
    {
        private const string SuppliersView = "Suppliers";

        private readonly ISupplierService suppliers;


        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;

        }

        public IActionResult Local()
        {
            return this.View(SuppliersView, this.GetSuppliers(false));
        }

        public IActionResult Importers()
        {
            return this.View(SuppliersView, this.GetSuppliers(true));
        }

        private SuppliersModel GetSuppliers(bool importers)
        {
            var type = importers ? "Importer" : "Local";

            var suppliers = this.suppliers.AllListings(importers);

            return new SuppliersModel
            {
                Suppliers = suppliers,
                Type = type
            };
        }
    }
}
