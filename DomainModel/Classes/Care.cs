using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes
{
    public class Care:AbstractProduct
    {       
        public string Type { get; set; }
    }
}
