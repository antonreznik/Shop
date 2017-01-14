using DomainModel.Classes.Colors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes
{
     public abstract class AbstractProduct
    {
         [Key]
         public int ProductId { get; set; }
         public string ProductName { get; set; }
         public int CategoryId { get; set; }
         public string CategoryName { get; set; }
         public string Description { get; set; }
         public decimal Price { get; set; }
         public byte[] ImageData { get; set; }
         public string ImageMimeType { get; set; }
         public List<Color> Colors { get; set; }
         public bool IsAvailable { get; set; }
         public int CountOfView { get; set; }
    }
}
