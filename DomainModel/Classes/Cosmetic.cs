using DomainModel.Classes.Colors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes
{
    public class Cosmetic:AbstractProduct
    {
        public string Type { get; set; }
        public string SubType { get; set; }
    }
}
