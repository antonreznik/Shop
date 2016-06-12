using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealShop.Models.Colors
{
    public class ColorViewModel
    {
        [HiddenInput(DisplayValue=false)]
        public int ColorId { get; set; }

        public string ColorName { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }
    }
}