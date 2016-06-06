using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealShop.Models
{
    public class CareViewModel
    {
        [HiddenInput(DisplayValue=false)]
        public int ProductId { get; set; }

        [Required(ErrorMessage="Заполните наименование товара")]
        public string ProductName { get; set; }

        [Required(ErrorMessage="Выберите тип средств для ухода")]
        public string Type { get; set; }
        public IEnumerable<SelectListItem> TypeList { get; set; }

        [Required(ErrorMessage = "Заполните описание товара")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "Заполните цену")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Не заполнено Id категории")]
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public CareViewModel()
        {
            List<string> types = new List<string>();
            types.Add("Уход за кожей лица");
            types.Add("Уход за телом");
            types.Add("Уход за губами");
            types.Add("Уход за кожей рук");

            TypeList = new SelectList(types);
        }
    }
}