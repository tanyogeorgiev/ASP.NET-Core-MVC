
namespace CarDealer.Services
{
    using Models;
    using System.Collections.Generic;

    public interface ICarService
    {
        IEnumerable<CarModel> ByMake(string make);
    }
}
