

namespace CarDealer.Services
{ 
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;

    public interface IPartService
    {
        IEnumerable<PartListingModel> All(int page = 1, int pageSize = 10);

        int Total();

        void Create(string name, decimal price, int quantity, int supplierId);

        PartListingModel ById(int Id);

        bool Exists(int id);
        void Edit(int id, string name, decimal price, int quantity, int supplierId);
    }
}
