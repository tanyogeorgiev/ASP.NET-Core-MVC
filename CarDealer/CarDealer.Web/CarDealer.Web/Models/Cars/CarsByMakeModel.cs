namespace CarDealer.Web.Models.Cars
{
    using System.Collections.Generic;
    using Services.Models;

    public class CarsByMakeModel
    {
        public string Make { get; set; }

        public IEnumerable<CarModel> Cars { get; set; }


    }
}
