using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Classes;
using DomainModel.Interfaces;
using RealShop.Models;
using RealShop.Extensions;
using AutoMapper;
using System.Net.Mail;
using Postal;
using RealShop.WebRequests.NewPostAPI;
using System.Threading;
using DomainModel.Entity;



namespace RealShop.Controllers
{
    public class CartController : Controller
    {
        IProductRepository productrepo;
        ICosmeticRepository cosmeticrepo;
        ICareRepository carerepo;
        IOrderRepository orderrepo;
        ICategoryRepository categoryrepo;
        ColorRepository colorrepo = new ColorRepository();
        public decimal Currency { get; set; }
        public CartViewModel Cart 
        { 
            get
            {
                if (Session["cart"] == null)
                {
                    return null;
                }

                else
                {
                    return Cart = Session["cart"] as CartViewModel;
                }
            } 

            set
            {
                Session["cart"] = value;
            }
        
        }
        public CartController(IProductRepository productrepo, IOrderRepository orderrepo, ICosmeticRepository cosmeticrepo, ICareRepository carerepo, ICategoryRepository categoryrepo)
        {
            this.productrepo = productrepo;
            this.cosmeticrepo = cosmeticrepo;
            this.carerepo = carerepo;
            this.orderrepo = orderrepo;
            this.categoryrepo = categoryrepo;
            Mapper.CreateMap<OrderViewModel, Order>();
            Mapper.CreateMap<RealShop.Models.OrderViewModel.ProductsInOrderViewModel, ProductsInOrder>();
            Currency = 32;
            /*Mapper.CreateMap<RealShop.Models.OrderViewModel.ParfumInOrderViewModel, ParfumInOrder>();
            Mapper.CreateMap<RealShop.Models.OrderViewModel.CosmeticInOrderViewModel, CosmeticInOrder>();
            Mapper.CreateMap<RealShop.Models.OrderViewModel.CareInOrderViewModel, CareInOrder>();*/
        }

        public ActionResult Index()
        {
            return View();
        }

       
        //-----------------------------------------------
        //Добавление товара в корзину
        public ActionResult AddProductToCart(int ProductId, int CategoryId, string Size, string ColorId)
        {
            AbstractProduct obj=null;
            //if (CategoryId == 1)
            //{
            //    Parfum parfum = productrepo.GetProductById(ProductId);

            //    if (Size == "50 ml")
            //    {
            //        parfum.Price = parfum.PriceFor50ml;
            //    }

            //    else if (Size == "75 ml")
            //    {
            //        parfum.Price = parfum.PriceFor75ml;
            //    }

            //    else if (Size == "100 ml")
            //    {
            //        parfum.Price = parfum.PriceFor100ml;
            //    }

            //    if (Cart == null)
            //    {
            //        Cart = new CartViewModel();
            //    }
            //    Cart.AddParfumToCart(parfum, Size);
            //    parfum.CategoryName = categoryrepo.GetCategoryName(parfum.CategoryId);
            //}

            if(CategoryId==2)
            {
                obj = cosmeticrepo.GetProductById(ProductId);
                obj.Price = Convert.ToInt32(obj.Price * Currency);
                if (obj.Colors.Count > 0)
                {
                    if (ColorId == null)
                    {
                        return PartialView("ErrorNoColorWasChosen");
                    }

                    else
                    {
                        int Colorid = Int32.Parse(ColorId);
                        obj.Colors.RemoveAll(color => color.ColorId != Colorid);
                    }
                    
                }
                
                if (Cart == null)
                {
                    Cart = new CartViewModel();
                }
                Cart.AddProductToCart(obj);
                obj.CategoryName = categoryrepo.GetCategoryName(obj.CategoryId);
            }

            if (CategoryId == 3)
            {
                obj = carerepo.GetProductById(ProductId);
                obj.Price = Convert.ToInt32(obj.Price * Currency);
                if (Cart == null)
                {
                    Cart = new CartViewModel();
                }
                Cart.AddProductToCart(obj);
                obj.CategoryName = categoryrepo.GetCategoryName(obj.CategoryId);
            }
            
            return RedirectToAction("ShowCartOnPage", "Cart"); 
        }

        //-----------------------------------------------
        //Удаление товара из корзины
        public ActionResult RemoveProductFromCart(int ProductId, int CategoryId, string Size, string ColorId)
        {
            AbstractProduct obj=null;
            if (CategoryId == 1) 
            {
                Parfum parfum = productrepo.GetProductById(ProductId);
                Cart.RemoveParfumFromCart(parfum,Size);
            }

            if (CategoryId==2)
            {
                obj = cosmeticrepo.GetProductById(ProductId);
                if (obj.Colors.Count > 0)
                {
                    int colorid = Int32.Parse(ColorId);
                    obj.Colors.RemoveAll(color => color.ColorId != colorid);
                }
                Cart.RemoveProductFromCart(obj);
            }

            if (CategoryId == 3)
            {
                obj = carerepo.GetProductById(ProductId);
                Cart.RemoveProductFromCart(obj);
            }
            //Cart.RemoveProductFromCart(obj);
            return RedirectToAction("ShowCartOnPage", "Cart");
        }

        //-------------------------------------------
        //Отображение корзины на главной странице с количеством товаров
        public ActionResult ShowCartOnPage()
        {
            InfoCartViewModel InfoCart = Cart.GetCartInfo();
            return View(InfoCart);
        }

        //-------------------------------------------
        //Отображение полной корзины
        public ActionResult ShowFullCart()
        {
            CartViewModel obj = Cart;
            if (Cart != null)
            {
                Cart.InfoCart = Cart.GetCartInfo();
            }
            return PartialView("ShowFullCart", obj);
        }

        //--------------------------------------
        //Подтверждение заказа
        public ActionResult MakeOrder()
        {
            OrderViewModel obj = new OrderViewModel();
            return View(obj);
        }

        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel obj)
        {
            Thread.Sleep(100);
            if (obj.NewPostCity != null && obj.NewPostOffice == null)
            {
                ModelState.AddModelError("NewPostOffice", "Выберите отделение для доставки");
            }

            //if (obj.DeliveryMethod == "NewPost" && obj.NewPostCity == null)
            //{
            //    ModelState.AddModelError("NewPostCity", "Выберите город");
            //}

            //if (obj.DeliveryMethod == "NewPost" && obj.NewPostOffice == null)
            //{
            //    ModelState.AddModelError("NewPostOffice", "Выберите отделение \"Новой почты\"");
            //}

            //if (obj.DeliveryMethod == "Courier" && obj.DeliveryCity == null)
            //{
            //    ModelState.AddModelError("DeliveryCity", "Введите город");
            //}

            //if (obj.DeliveryMethod == "Courier" && obj.DeliveryAdress == null)
            //{
            //    ModelState.AddModelError("DeliveryAdress", "Введите адрес доставки");
            //}

            if (ModelState.IsValid)
            {  
                //if(obj.DeliveryMethod=="NewPost")
                //{
                //    obj.DeliveryCity = null;
                //    obj.DeliveryAdress = null;
                //}

                //if (obj.DeliveryMethod=="Courier")
                //{
                //    obj.NewPostCity = null;
                //    obj.NewPostOffice = null;
                //}

                CartViewModel cart = Cart;
                obj.TotalPriceOfAllProducts = cart.InfoCart.TotalPriceOfProducts;
                obj.TotalQuantityOfAllParfums = cart.InfoCart.TotalQuantityOfProducts;

                foreach (var item in cart.Products)
                {
                    RealShop.Models.OrderViewModel.ProductsInOrderViewModel product = new RealShop.Models.OrderViewModel.ProductsInOrderViewModel();
                    product.CategoryId = item.obj.CategoryId;
                    product.CategoryName = item.obj.CategoryName;
                    product.ProductId = item.obj.ProductId;
                    product.ProductName = item.obj.ProductName + " " + item.Size;
                    if (item.Color != null)
                    {
                        product.ProductName += " " + item.Color.ColorName;
                    }
                    product.ImageData = item.obj.ImageData;
                    product.ImageMimeType = item.obj.ImageMimeType;
                    product.Quantity = item.Quantity;
                    product.TotalPrice = item.TotalPrice;
                    obj.ProductInOrder.Add(product);
                }

                Order ord = Mapper.Map<Order>(obj);
                if (ord == null)
                {
                    return PartialView(obj);
                }
                ord.OrderState = "New";
                obj.OrderId = orderrepo.AddOrder(ord);
                cart.RemoveAllFromCart();

                //Отправка почты при успешной валидации модели
                var Email = new EmailViewModel
                {
                    order = obj
                };
                
                Email.Send();
                return PartialView("MakeOrderPost",obj);
            }

            else
            {
                return PartialView(obj);
            }
        }

        // Получение списка отделений Новой почты по выбраному городу
        public ActionResult GetNewPostOffices(string City, string PostOffice)
        {
            NewPostOfficesViewModel offices = new NewPostOfficesViewModel(City);
            return View(offices);
        }     
    }
}
