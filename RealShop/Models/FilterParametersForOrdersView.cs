using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealShop.Models
{
    public class FilterParametersForOrdersView
    {    
        [Required]
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string PhoneNumber { get; set; }
        public string NewPostRef { get; set; }
    }
}