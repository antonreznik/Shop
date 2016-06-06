using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealShop.Models
{
    public class CategoryViewModelForDropDownList
    {
        public string CategoryId { get; set; }
        public IEnumerable<SelectListItem> List { get; set; }
    }
}