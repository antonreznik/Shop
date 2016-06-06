using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes
{
    public class Parfum:AbstractProduct
    {
        public string Sex { get; set; }
        public bool Size25ml { get; set; }
        public bool Size50ml { get; set; }
        public bool Size75ml { get; set; }
        public bool Size100ml { get; set; }
        public decimal PriceFor50ml { get; set; }
        public decimal PriceFor75ml { get; set; }
        public decimal PriceFor100ml { get; set; }
    }
}
