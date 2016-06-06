using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealShop.Models
{
    public class CosmeticViewModel
    {
        [HiddenInput(DisplayValue=false)]
        public int ProductId { get; set; }

        [Required(ErrorMessage="Заполните наименование товара")]
        public string ProductName { get; set; }

        [Required(ErrorMessage="Выберите тип косметики")]
        public string Type { get; set; }
        public IEnumerable<SelectListItem> TypeList { get; set; }

        [Required(ErrorMessage="Выберите подтип косметики")]
        public string SubType { get; set; }

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

        public CosmeticViewModel()
        {
            List<string> types = new List<string>();
            types.Add("Для глаз");
            types.Add("Для губ");
            types.Add("Тональные средства");
            types.Add("Для ногтей");

            TypeList = new SelectList(types);
        }
    }
}