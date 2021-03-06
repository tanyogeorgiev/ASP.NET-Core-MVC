﻿
namespace CarDealer.Services
{
    using Data.Models;
    using Models.Cars;
    using System.Collections.Generic;


    public interface ICarService
    {
        IEnumerable<CarModel> ByMake(string make);

        IEnumerable<CarWithPartsModel> WithParts();

        void Create(string make, string model, long travelledDistance, int[] partsList);
    }
}
