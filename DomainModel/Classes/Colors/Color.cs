using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes.Colors
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
