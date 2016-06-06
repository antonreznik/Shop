using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Classes;

namespace RealShop.Models
{
    public class ParfumViewModel
    {
        [HiddenInput(DisplayValue=false)]
        public int ProductId { get; set; }
        
        [Required(ErrorMessage="Заполните наименование товара")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Заполните для какого пола товар")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Заполните для какого пола товар")]
        public IEnumerable<SelectListItem> List { get; set; }
        
        [Required(ErrorMessage = "Заполните описание товара")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage="Заполните цену")]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = "Заполните размер")]
        //public string Size { get; set; }
        //[Required(ErrorMessage = "Заполните размер")]
        //public IEnumerable<SelectListItem> Ml { get; set; }

        public bool Size25ml { get; set; }
        public bool Size50ml { get; set; }
        public bool Size75ml { get; set; }
        public bool Size100ml { get; set; }
        public decimal PriceFor50ml { get; set; }
        public decimal PriceFor75ml { get; set; }
        public decimal PriceFor100ml { get; set; }

        [Required(ErrorMessage="Не заполнено Id категории")]
        [HiddenInput (DisplayValue=false)]
        public int CategoryId { get; set; }

        public byte [] ImageData { get; set; }

        [HiddenInput (DisplayValue=false)]
        public string ImageMimeType { get; set; }

        public ParfumViewModel()
        {
            List<string> SexL = new List<string>();
            SexL.Add("Men");
            SexL.Add("Women");
            SexL.AsEnumerable();
            List = new SelectList(SexL);

            //List<double> Ml = new List<double>();
            //Ml.Add(0.25);
            //Ml.Add(0.50);
            //Ml.Add(0.75);
            //Ml.Add(100);
            //this.Ml = new SelectList(Ml);
        }
    }
}