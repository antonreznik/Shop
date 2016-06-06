using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;
using System.ComponentModel.DataAnnotations;

namespace RealShop.Models
{
    public class EmailViewModel:Email
    {
        public OrderViewModel order { get; set; }
    }
}