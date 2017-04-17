using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryAdress { get; set; }
        public string NewPostCity { get; set; }
        public string NewPostOffice { get; set; }
        public decimal TotalPriceOfAllProducts { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime DateOfSendingOrderToClient { get; set; }
        public string NewPostRef { get; set; }
        public string OrderState { get; set; }
        public double DeliveryPrice { get; set; }
        public virtual ICollection<ProductsInOrder> ProductInOrder { get; set; }
    }
}
