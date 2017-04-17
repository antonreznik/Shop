using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealShop.Models
{
    public class InfoCartViewModel
    {
        public int TotalQuantityOfProducts { get; set; }
        public double TotalPriceOfProducts { get; set; }
        public double TotalPriceWithDelivery { get; set; }
    }
}