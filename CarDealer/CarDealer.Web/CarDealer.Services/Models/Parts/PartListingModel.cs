﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services.Models.Parts
{
   public class PartListingModel : PartModel
    {
        public int Quantity { get; set; }

        public int SupplierId { get; set; }

        public string SupplierName { get; set; }
    }
}
