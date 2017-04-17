using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Classes;
using RealShop.WebRequests.NewPostAPI;
using System.Collections;

namespace RealShop.Models
{
    public class OrderViewModel
    {
        [HiddenInput(DisplayValue=false)]
        public int OrderId { get; set; }

        [HiddenInput(DisplayValue=false)]
        //[DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage="Введите фамилию")]
        public string Surname { get; set; }

        [Required(ErrorMessage="Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage="Введите отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage="Введите номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required (ErrorMessage="Введите E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Required(ErrorMessage="Выберите способ доставки")]
        //public string DeliveryMethod { get; set; }

        public string DeliveryCity { get; set; }
        public string DeliveryAdress { get; set; }

        [Required(ErrorMessage="Выберите город для доставки")]
        public string NewPostCity { get; set; }
        public IEnumerable<SelectListItem> NewPostCities { get; set; }

        public string NewPostOffice { get; set; }
        
        //[Required(ErrorMessage="Выберите способ оплаты")]
        //public string PaymentMethod { get; set; }

        //public string CardPay { get; set; }
        //public string PostPay { get; set; }

        [HiddenInput(DisplayValue = false)]
        public List<ProductsInOrderViewModel> ProductInOrder { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int TotalQuantityOfAllParfums { get; set; }

        [HiddenInput(DisplayValue = false)]
        public double TotalPriceOfAllProducts { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime DateOfSendingOrderToClient { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string NewPostRef { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string OrderState { get; set; }

        [HiddenInput(DisplayValue = false)]
        public double DeliveryPrice { get; set; }

        //--------------------------------------------
        //Класс для товаров в корзине
        public class ProductsInOrderViewModel
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public byte[] ImageData { get; set; }
            public string ImageMimeType { get; set; }
            public int Quantity { get; set; }
            public double TotalPrice { get; set; }
        }

        //Конструктор
        public OrderViewModel()
        {
            List<string> empty = new List<string>();
            ProductInOrder = new List<ProductsInOrderViewModel>();
            NewPostCities = new SelectList(RequestToNewPostApi.NewPostCities());
            OrderDate = DateTime.Now;
            
        }

    }
}