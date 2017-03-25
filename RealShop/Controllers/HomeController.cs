using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DomainModel.Classes;
using DomainModel.Interfaces;
using RealShop.Models;
using AutoMapper;

namespace RealShop.Controllers
{
    public class HomeController : Controller
    {
        private ICategoryRepository CategoryRepo;
        private IProductRepository ProductRepo;
        private ICosmeticRepository CosmeticRepo;
        private ICareRepository CareRepo;
        private IDataForPrice DataForPriceRepo;
        //--------------------------------------------------
        //Конструктор
        public HomeController(ICategoryRepository CategoryRepo, IProductRepository ProductRepo, ICosmeticRepository CosmeticRepo, ICareRepository CareRepo, IDataForPrice DataForPriceRepo)
        {
            this.CategoryRepo = CategoryRepo;
            this.ProductRepo = ProductRepo;
            this.CosmeticRepo = CosmeticRepo;
            this.CareRepo = CareRepo;
            this.DataForPriceRepo = DataForPriceRepo;
            Mapper.CreateMap<ModelForCosmeticFilter, CosmeticFilter>();
        }
        
        public ActionResult Index()
        {
            return View();
        }

        //---------------------------------------------------        
        //Достаем категории для клиента
        public ActionResult GetCategoriesToClient()
        {
            IQueryable<Category>Categories = CategoryRepo.GetAllCategoriesFromBase;
            return View(Categories);
        }

        //----------------------------------------------------
        //Достаем все продукты для клиента
        public ActionResult GetProductsToClient(int CategoryId)
        {
            IQueryable<AbstractProduct> products = null;
            if (CategoryId == 1)
            {
                products = ProductRepo.GetProductsFromCategory(CategoryId);
                return View("GetParfumsToClient", products);
            }

            else if (CategoryId == 2)
            {
                products = CosmeticRepo.GetProductsFromCategory(CategoryId);
            }

            else if (CategoryId == 3)
            {
                products = CareRepo.GetProductsFromCategory(CategoryId);
            }
            products.ToList().ForEach(x => x.PriceToShow = BuildPrice(x.NewPrice));
            return View("GetProductsToClient", products);         
           
        }

        //----------------------------------------------------
        //Достаем часть продуктов для клиента при первой загрузке сайта
        public ActionResult GetNotAllProductsToClient(int CategoryId)
        {
            IQueryable<AbstractProduct> products = null;
            if (CategoryId == 1)
            {
                products = ProductRepo.GetProductsFromCategory(CategoryId);
                return View("GetParfumsToClient", products);
            }

            else if (CategoryId == 2)
            {
                products = CosmeticRepo.GetNotAllProductsFromCategory(CategoryId);
            }

            else if (CategoryId == 3)
            {
                products = CareRepo.GetNotAllProductsFromCategory(CategoryId);
            }

            products.ToList().ForEach(x => x.PriceToShow = BuildPrice(x.NewPrice));
            return View("GetProductsToClient", products);

        }

        //----------------------------------------------------
        //Фильтр для парфюма
        public ActionResult FilterCategoryId1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FilterCategoryId1(bool IsMen, bool IsWomen, bool Size25, bool Size50, bool Size75, bool Size100)
        { 
            IQueryable<Parfum> Products = ProductRepo.GetProductByFilters(IsMen, IsWomen, Size25, Size50, Size75, Size100);
            return PartialView("GetParfumsToClient", Products);
        }

        //----------------------------------------------------
        //Фильтр для косметики
        public ActionResult FilterCategoryId2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FilterCategoryId2(ModelForCosmeticFilter obj)
        {
            CosmeticFilter filt = Mapper.Map<CosmeticFilter>(obj);
            IQueryable<Cosmetic> Cosmetics = CosmeticRepo.GetProductByFilters(filt);
            Cosmetics.ToList().ForEach(x => x.PriceToShow = BuildPrice(x.NewPrice));
            return PartialView("GetProductsToClient", Cosmetics);
        }

        //---------------------------------------------------
        //Фильтр для средств ухода
        public ActionResult FilterCategoryId3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FilterCategoryId3(ModelForCareFilter obj)
        {
            IQueryable<Care> Cares = CareRepo.GetProductByFilters(obj.FaceSkin, obj.BodyCare, obj.LipsCare, obj.HandCare);
            Cares.ToList().ForEach(x => x.PriceToShow = BuildPrice(x.NewPrice));
            return PartialView("GetProductsToClient", Cares);
        }


        //----------------------------------------------------
        //Метод для отображения информации по одному товару при клике на его изображение
        public ActionResult GetOneProductInfo(int ProductId, int CategoryId)
        {
            AbstractProduct obj = null;
            if (CategoryId == 1)
            {
                Parfum parfum = ProductRepo.GetProductById(ProductId);
                return PartialView("GetOneParfumInfo", parfum);
            }

            else if (CategoryId == 2)
            {
                obj = CosmeticRepo.GetProductById(ProductId);
                obj.PriceToShow = BuildPrice(obj.NewPrice);
                CosmeticRepo.AddCountOfView(ProductId);
                return PartialView("GetOneProductInfo", obj);
            }

            else if (CategoryId == 3)
            {
                obj = CareRepo.GetProductById(ProductId);
                obj.PriceToShow = BuildPrice(obj.NewPrice);
                CareRepo.AddCountOfView(ProductId);
                return PartialView("GetOneProductInfo", obj);
            }

            //return PartialView("GetOneProductInfo", obj);
            return View();
        }

        //----------------------------------------------------
        //Метод для отображения информации при клике на закладку "О продукции"
        public ActionResult AboutProduction ()
        {
            return View();
        }

        //----------------------------------------------------
        //Метод для отображения информации при клике на закладку "Доставка"
        public ActionResult DeliveryInfo()
        {
            return View();
        }

        //----------------------------------------------------
        //Метод для отображения информации при клике на закладку "Оформление заказа"
        public ActionResult MakeOrderInfo ()
        {
            return View();
        }

        //----------------------------------------------------
        //Метод для отображения информации про контакты
        public ActionResult Contacts ()
        {
            return View();
        }

        //-----------------------------------------
        //Формирование цены
        private int BuildPrice(double price)
        {
            if(price > 0)
            {
                var dataForPrice = DataForPriceRepo.GetData();
                return Convert.ToInt32(price * dataForPrice.PriceCoefficient
                             * dataForPrice.Currency
                             * dataForPrice.PostPercentComission
                             + dataForPrice.PostFixedComission);
            }

            else
            {
                return 0;
            }
            
        }
    }
}
