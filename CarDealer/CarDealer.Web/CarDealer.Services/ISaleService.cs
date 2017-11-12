using System.Collections.Generic;

namespace CarDealer.Services
{
    using Models.Sales;
   public interface ISaleService
    {
        IEnumerable<SaleListModel> All();
    }
}
