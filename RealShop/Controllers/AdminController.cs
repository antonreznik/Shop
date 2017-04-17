using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DomainModel.Classes;
using DomainModel.Entity;
using DomainModel.Interfaces;
using RealShop.Models;
using System.Globalization;

namespace RealShop.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ICategoryRepository repo;
        private IProductRepository productrepo;
        private IOrderRepository orderrepo;
        private ICosmeticRepository cosmeticrepo;
        private ICareRepository carerepo;
        private IColor colorrepo;

        public AdminController(ICategoryRepository repo, IProductRepository productrepo, ICosmeticRepository cosmeticrepo, IOrderRepository orderrepo, ICareRepository carerepo, IColor colorrepo)
        {
            this.repo = repo;
            this.productrepo = productrepo;
            this.orderrepo = orderrepo;
            this.cosmeticrepo = cosmeticrepo;
            this.carerepo = carerepo;
            this.colorrepo = colorrepo;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.CreateMap<ParfumViewModel, Parfum>();
                cfg.CreateMap<Parfum, ParfumViewModel>();
                cfg.CreateMap<Cosmetic, CosmeticViewModel>();
                cfg.CreateMap<CosmeticViewModel, Cosmetic>();
                cfg.CreateMap<Care, CareViewModel>();
                cfg.CreateMap<CareViewModel, Care>();
                cfg.CreateMap<FilterParametersForOrdersView, FilterParametersForOrders>();
                cfg.CreateMap<Order, OrderViewModel>();

            });
            Mapper.CreateMap<Category, CategoryViewModel>();
            Mapper.CreateMap<CategoryViewModel, Category>();
            Mapper.CreateMap<ParfumViewModel, Parfum>();
            Mapper.CreateMap<Parfum, ParfumViewModel>();
            Mapper.CreateMap<Cosmetic, CosmeticViewModel>();
            Mapper.CreateMap<CosmeticViewModel,Cosmetic>();
            Mapper.CreateMap<Care, CareViewModel>();
            Mapper.CreateMap<CareViewModel, Care>();
            Mapper.CreateMap<FilterParametersForOrdersView, FilterParametersForOrders>();
            Mapper.CreateMap<Order, OrderViewModel>();
            Mapper.CreateMap<ProductsInOrder, OrderViewModel.ProductsInOrderViewModel>();
        }

        //---------------------------------------------------
        //Главная страница администратора
        public ActionResult Index()
        {
            return View();
        }
        //---------------------------------------------------
        
        
        //---------------------------------------------------
        //Отображение всех категорий из базы
        public ActionResult Categories()
        {
            var CategoriesView = repo.GetAllCategoriesFromBase;
            return View(CategoriesView);
        }
        //---------------------------------------------------
        

        //---------------------------------------------------
        //Добавление новой категории
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                Category CategoryObj = Mapper.Map<Category>(obj);
                repo.SaveCategory(CategoryObj);
                TempData["message"] = String.Format("Категория '{0}' успешно добавлена", obj.CategoryName);
                return RedirectToAction("Categories","Admin");
            }
            else
            {
                return View();
            }
            
        }
        //----------------------------------------------------


        //----------------------------------------------------
        //Редактирование существующих категорий     
        public ActionResult EditCategory(int CategoryId)
        {
            Category obj = repo.GetAllCategoriesFromBase.FirstOrDefault(category => category.CategoryId == CategoryId);
            CategoryViewModel ViewCategory = Mapper.Map<CategoryViewModel>(obj);
            return View(ViewCategory);
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel obj)
        {
            if (ModelState.IsValid)
            {
                Category CategoryObj = Mapper.Map<Category>(obj);
                repo.SaveCategory(CategoryObj);
                TempData["message"] = String.Format("Категория '{0}' успешно отредактирована", obj.CategoryName);
                return RedirectToAction("Categories", "Admin");
            }
            else
            {
                return View();
            }
            
        }
        //-----------------------------------------------------

        
        //-----------------------------------------------------
        //Удаление категории
        public ActionResult DeleteCategory(int CategoryId)
        {
            Category obj = repo.GetAllCategoriesFromBase.FirstOrDefault(category => category.CategoryId == CategoryId);
            TempData["message"] = repo.DeleteCategory(obj);
            return RedirectToAction("Categories", "Admin");
        }
        //-----------------------------------------------------


        //-----------------------------------------------------
        //Добавление парфюма в категорию
        public ActionResult AddProduct(int CategoryId)
        {
            TempData["CategoryName"] = repo.GetCategoryName(CategoryId);
            ParfumViewModel model = new ParfumViewModel();
            return View(model);
        }

        
        [HttpPost]
        public ActionResult AddProduct(ParfumViewModel obj, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    obj.ImageMimeType = Image.ContentType;
                    obj.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(obj.ImageData, 0, Image.ContentLength);
                }
                Parfum prod = Mapper.Map<Parfum>(obj);
                productrepo.SaveProduct(prod);
                TempData["message"] = String.Format("Товар '{0}' добавлен", obj.ProductName);
                return RedirectToAction("Categories", "Admin");
            }

            else
            {
                return View(obj);
            }
        }

        //------------------------------------------------------
        //Добавление косметики в категорию
        public ActionResult AddCosmetic (int CategoryId)
        {
            TempData["CategoryName"] = repo.GetCategoryName(CategoryId);
            CosmeticViewModel cosmetic = new CosmeticViewModel();
            return View("AddCosmetic", cosmetic);
        }
        
        [HttpPost]
        public ActionResult AddCosmetic(CosmeticViewModel obj, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    obj.ImageMimeType = Image.ContentType;
                    obj.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(obj.ImageData, 0, Image.ContentLength);
                }
                Cosmetic prod = Mapper.Map<Cosmetic>(obj);
                cosmeticrepo.SaveProduct(prod);
                TempData["message"] = String.Format("Товар '{0}' добавлен", obj.ProductName);
                return RedirectToAction("Categories", "Admin");
            }

            else
            {
                return View(obj);
            }
        }

        //Добавление средств для ухода в категорию
        public ActionResult AddCare(int CategoryId)
        {
            TempData["CategoryName"] = repo.GetCategoryName(CategoryId);
            CareViewModel care = new CareViewModel();
            return View("AddCare", care);
        }

        [HttpPost]
        public ActionResult AddCare(CareViewModel obj, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    obj.ImageMimeType = Image.ContentType;
                    obj.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(obj.ImageData, 0, Image.ContentLength);
                }
                Care prod = Mapper.Map<Care>(obj);
                carerepo.SaveProduct(prod);
                TempData["message"] = String.Format("Товар '{0}' добавлен", obj.ProductName);
                return RedirectToAction("Categories", "Admin");
            }

            else
            {
                return View(obj);
            }
        }

        //-----------------------------------------------------
        //Отображение всех товаров по конкретной категории
        public ActionResult ShowProductsInCategory(int CategoryId)
        {
            TempData["CategoryName"] = repo.GetCategoryName(CategoryId);
            if (CategoryId == 1)
            {
                IQueryable<Parfum> Products = productrepo.GetProductsFromCategory(CategoryId);
                return View(Products);
            }

            else if (CategoryId==2)
            {
                IQueryable<Cosmetic> Cosmetics = cosmeticrepo.AdminGetProductsFromCategory(CategoryId);
                return View("ShowCosmeticsInCategory", Cosmetics);
            }

            else if (CategoryId==3)
            {
                IQueryable<Care> Cares = carerepo.AdminGetProductsFromCategory(CategoryId);
                return View("ShowCaresInCategory", Cares);
            }

            return View();
        }

        //-----------------------------------------------------
        //Редактирование парфюма
        public ActionResult EditProduct(int ProductId)
        {
            Parfum product = productrepo.GetProductById(ProductId);
            ParfumViewModel obj = Mapper.Map<ParfumViewModel>(product);
            return View (obj);
        }

        [HttpPost]
        public ActionResult EditProduct(ParfumViewModel obj, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    obj.ImageMimeType = Image.ContentType;
                    obj.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(obj.ImageData, 0, Image.ContentLength);
                }
                Parfum p = Mapper.Map<Parfum>(obj);
                productrepo.SaveProduct(p);
                TempData["message"] = String.Format("Изменения в товаре '{0}' сохранены", obj.ProductName);
                return RedirectToAction("ShowProductsInCategory", "Admin", new {obj.CategoryId});
            }

            else
            {
                return View(obj);
            }
        }

        //-----------------------------------------------------
        //Редактирование косметики
        public ActionResult EditCosmetic(int ProductId)
        {
            Cosmetic cosmetic = cosmeticrepo.GetProductById(ProductId);
            CosmeticViewModel obj = Mapper.Map<CosmeticViewModel>(cosmetic);
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditCosmetic(CosmeticViewModel obj, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    obj.ImageMimeType = Image.ContentType;
                    obj.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(obj.ImageData, 0, Image.ContentLength);
                }
                Cosmetic p = Mapper.Map<Cosmetic>(obj);
                cosmeticrepo.SaveProduct(p);
                TempData["message"] = String.Format("Изменения в товаре '{0}' сохранены", obj.ProductName);
                return RedirectToAction("ShowProductsInCategory", "Admin", new { obj.CategoryId });
            }

            else
            {
                return View(obj);
            }
        }


        //------------------------------------------------------
        //Редактирование средств по уходу
        public ActionResult EditCare(int ProductId)
        {
            Care care = carerepo.GetProductById(ProductId);
            CareViewModel obj = Mapper.Map<CareViewModel>(care);
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditCare(CareViewModel obj, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    obj.ImageMimeType = Image.ContentType;
                    obj.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(obj.ImageData, 0, Image.ContentLength);
                }
                Care p = Mapper.Map<Care>(obj);
                carerepo.SaveProduct(p);
                TempData["message"] = String.Format("Изменения в товаре '{0}' сохранены", obj.ProductName);
                return RedirectToAction("ShowProductsInCategory", "Admin", new { obj.CategoryId });
            }

            else
            {
                return View(obj);
            }
        }


        //------------------------------------------------------
        //Удаление товара
        public ActionResult DeleteProduct(int ProductId, int CategoryId)
        {
            if (CategoryId == 1)
            {
                Parfum obj = productrepo.GetProductById(ProductId);
                productrepo.DeleteProduct(obj);
                TempData["message"] = String.Format("Товар '{0}' удален из базы", obj.ProductName);
                return RedirectToAction("ShowProductsInCategory", "Admin", new { obj.CategoryId });
            }

            else if (CategoryId==2)
            {
                Cosmetic obj = cosmeticrepo.GetProductById(ProductId);
                cosmeticrepo.DeleteProduct(obj);
                TempData["message"] = String.Format("Товар '{0}' удален из базы", obj.ProductName);
                return RedirectToAction("ShowProductsInCategory", "Admin", new { obj.CategoryId });
            }

            else if (CategoryId==3)
            {
                Care obj = carerepo.GetProductById(ProductId);
                carerepo.DeleteProduct(obj);
                TempData["message"] = String.Format("Товар'{0}'удвлен из базы", obj.ProductName);
                return RedirectToAction("ShowProductsInCategory", "Admin", new { obj.CategoryId });
            }

            return View();
            
        }

        //------------------------------------------------------
        //Достаем картинки товара
        [AllowAnonymous]
        public FileContentResult GetImage(int ProductId, int CategoryId)
        {
            if (CategoryId == 1)
            {
                Parfum obj = productrepo.GetProductById(ProductId);
                if (obj != null)
                {
                    return File(obj.ImageData, obj.ImageMimeType);
                }

                else
                {
                    return null;
                }
            }

            else if (CategoryId == 2)
            {
                Cosmetic obj = cosmeticrepo.GetProductById(ProductId);
                if (obj != null)
                {
                    return File(obj.ImageData, obj.ImageMimeType);
                }

                else
                {
                    return null;
                }
            }

            else if (CategoryId == 3)
            {
                Care obj = carerepo.GetProductById(ProductId);
                if (obj != null)
                {
                    return File(obj.ImageData, obj.ImageMimeType);
                }

                else
                {
                    return null;
                }
            }

            return null;
        }


        //------------------------------------------------------
        //Изменение категории в товаре

        //public ActionResult ChangeCategoryInProduct(int ProductId)
        //{
        //    SelectList Categories = new SelectList(repo.GetAllCategoriesFromBase,"CategoryId","CategoryName");
        //    var model = new CategoryViewModelForDropDownList
        //    {
        //        List = Categories.AsEnumerable()
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult ChangeCategoryInProduct(int ProductId, int CategoryId)
        //{
        //    Parfum obj = productrepo.GetProductById(ProductId);
        //    string CategoryName = repo.GetCategoryName(CategoryId);
        //    int OldCategoryId = obj.CategoryId;
        //    obj.CategoryId = CategoryId;
        //    productrepo.SaveProduct(obj);
        //    TempData["message"] = String.Format("Товар '{0}' перемещен в категорию {1}", obj.ProductName,CategoryName);
        //    return RedirectToAction("ShowProductsInCategory", "Admin", new { CategoryId });
        //}

        //Инициализация подтипов косметики для списка косметики

        //-------------------------------------------------------------------
        //Получаем подтипы для косметики
        public ActionResult GetSubTypesCosmetic(string type)
        {
            SubTypeCosmeticViewModel obj = new SubTypeCosmeticViewModel(type);
            return View(obj);
        }


        //Выбор всех заказов из базы
        public ActionResult GetOrdersFromBase()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetOrdersFromBase (FilterParametersForOrdersView obj)
        {
            if (obj.To == DateTime.MinValue)
            {
                obj.To = DateTime.Now.AddDays(1);
                //string DateString = obj.To.ToString();
                //obj.To = DateTime.ParseExact(DateString,"ddmmyyyy",CultureInfo.InvariantCulture);
                //obj.To.ToString("yyyy/mm/dd");
            }
            FilterParametersForOrders parameters = Mapper.Map<FilterParametersForOrders>(obj);
            IQueryable<Order> orders = orderrepo.GetOrdersFromBase(parameters);
            return View("GetOrdersFromBase(ReturnResult)",orders);
        }

        //Обработка заказа        
        public ActionResult OrderProcessing(string sentDate, string newPostRef, string orderId)
        {
            
            if (sentDate.Length==0 || newPostRef.Length==0)
            {
                return PartialView("OrderProcessingError");
            }
                
            orderrepo.CloseOrder(sentDate, newPostRef, orderId);
            return PartialView();
        }

        //Получение товаров по конкретному заказу
        [HttpPost]
        public ActionResult GetProductsInOrder(int orderId)
        {
            var viewModel = Mapper.Map<Order, OrderViewModel>(orderrepo.GetOrderById(orderId));
            return PartialView("GetProductsInOrder", viewModel);
        }

        //Проставление признака о доступности товара
        public void ChangeAvailabilityOfProduct(string productId, string categoryId, bool isAvailable)
        {
            if (Int32.Parse(categoryId) == 2)
            {
                cosmeticrepo.ChangeAvailability(Int32.Parse(productId), isAvailable);
            }

            else if (Int32.Parse(categoryId) == 3)
            {
                carerepo.ChangeAvailability(Int32.Parse(productId), isAvailable);
            }
        }

        //Проставление признака о доступности цвета
        public void ChangeAvailabilityOfColor(string colorId, bool isAvailable)
        {
            colorrepo.ChangeAvailability(Int32.Parse(colorId), isAvailable);
        }
    }
}
