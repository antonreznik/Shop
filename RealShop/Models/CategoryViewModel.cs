using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Classes;

namespace RealShop.Models
{
    public class CategoryViewModel
    {
        [HiddenInput (DisplayValue=false)]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage="Введите наименование категории")]
        public string CategoryName { get; set; }

        public virtual IEnumerable<Parfum> Products { get; set; }      
    }
}