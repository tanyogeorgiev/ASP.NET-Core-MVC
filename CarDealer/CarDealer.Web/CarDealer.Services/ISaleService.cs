using System.Collections.Generic;

namespace CarDealer.Services
{
   using Models.Sales;

   public interface ISaleService
    {
        IEnumerable<SaleListModel> All();
        SaleDetailsModel Details(int id);
        IEnumerable<SaleListModel> Discounted();
    }
}
